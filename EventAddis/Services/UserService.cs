using AutoMapper;
using EventAddis.Data;
using EventAddis.Dto;
using EventAddis.Models;
using EventAddis.Repositories;
using EventAddis.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventAddis.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IHashService _hashService;

        public UserService(DataContext context, IMapper mapper, IHashService hashService)
        {
            _context = context;
            _mapper = mapper;
            _hashService = hashService;
        }

        public async Task<bool> CreateUser(UserDetailsDto userDetails)
        {
            var userMap = _mapper.Map<UserInfo>(userDetails);
            userMap.UserId = Guid.NewGuid();
            userMap.RegisteredAt = DateTime.Now;

            var credentialMap = _mapper.Map<UserCredential>(userDetails);
            var encryptedCredentials = _hashService.HashPassword(userDetails.Password);

            credentialMap.CredentialId = Guid.NewGuid();
            credentialMap.HashedPassword = encryptedCredentials["hash"];
            credentialMap.Salt = encryptedCredentials["salt"];
            credentialMap.UserId = userMap.UserId;

            _context.Add(userMap);
            _context.Add(credentialMap);

            return await Save();
        }

        public async Task<ICollection<UserInfo>> GetUserInfos()
        {
            return await _context.UserInfos.OrderBy(u => u.UserId).Include(u => u.UserCredential).ToListAsync();
        }

        public async Task<bool> UserInfoExist(Guid id)
        {
            return await _context.UserInfos.AnyAsync(u => u.UserId == id);
        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }
    }
}
