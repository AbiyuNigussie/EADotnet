﻿using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebService.API.Data;
using WebService.API.Entity;
using WebService.API.Models;
using WebService.API.Repository;

namespace WebService.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _config;
        private readonly ApplicationDbContext _context;

        public AuthService(IConfiguration config, ApplicationDbContext context)
        {
            _config = config;
            _context = context;
        }

        public (PasswordVerificationResult passwordVerificationResult,UserInfo userDetails) Authenticate(AuthUser auth)
        {
            var user = _context.UserInfos.FirstOrDefault(x => x.Email.TrimEnd().ToLower() == auth.Email.Trim().ToLower() || x.Username.TrimEnd().ToLower() == auth.Email.Trim().ToLower());

            // check if username exists
            if (user != null)
            {
                var credUser = _context.UserCredentials.SingleOrDefault(x => x.UserId == user.UserId);

                // check if password is correct
                if (VerifyPasswordHash(auth.Password, credUser.PasswordHash, credUser.PasswordSalt))
                {
                    user.LastLogin = DateTime.UtcNow;
                    _context.SaveChanges();
                    return (PasswordVerificationResult.Success, user);
                }

                return (PasswordVerificationResult.Failed, user);
            }

            // authentication failed
            return (PasswordVerificationResult.Failed, null);
        }

        public string Generate(UserInfo user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(100),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}
