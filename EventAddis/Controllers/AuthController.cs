﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebService.API.Entity;
using WebService.API.Models;
using WebService.API.Repository;
using WebService.API.Services;

namespace WebService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;
        private readonly IUserService _userservice;
        private readonly IMapper _mapper;

        public AuthController(IAuthService AuthService, IUserService userService, IMapper mapper)
        {
            _auth = AuthService;
            _userservice = userService;
            _mapper = mapper;
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("Authentication")]
        public IActionResult Post([FromBody] AuthUser authentication)
        {

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = _auth.Authenticate(authentication);


            if (result.userDetails != null)
            {

                if (result.passwordVerificationResult == PasswordVerificationResult.Success)
                {
                    var token = _auth.Generate(result.userDetails);

                    return Ok(new
                    {
                        Id = result.userDetails.UserId,
                        Username = result.userDetails.Username,
                        Email = result.userDetails.Email,
                        Role = result.userDetails.Role,
                        Phone = result.userDetails.PhoneNo,
                        Created_at = DateTime.UtcNow,
                        Token = token
                    });
                }

                ModelState.AddModelError("errorMessage", "Incorrect Email/Password");
                return StatusCode(402, ModelState);


            }

            ModelState.AddModelError("errorMessage", "User not Found!");
            return StatusCode(404, ModelState);

        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Register")]
        public IActionResult createUser([FromBody] RegisterUser user)
        {
            if (user.Role != "User")
            {
                ModelState.AddModelError("", "Invalid role. Only 'user' role is allowed.");
                return StatusCode(422, ModelState);
            }

            var existingUser = _userservice.GetUsers().Where(u => u.Email.TrimEnd().ToLower() == user.Email.Trim().ToLower() || u.Username.TrimEnd().ToLower() == user.Username.Trim().ToLower()).FirstOrDefault();

            if(existingUser != null)
            {
                ModelState.AddModelError("", "Email/Useraname Already Exists!");
                return StatusCode(422, ModelState);
            }

            var createUser = _userservice.PostUser(user, user.Password);
            return Ok(createUser);
        }
    }
}
