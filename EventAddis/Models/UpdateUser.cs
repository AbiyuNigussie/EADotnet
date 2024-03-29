﻿using System.ComponentModel;
using WebService.API.Entity;

namespace WebService.API.Models
{
    public class UpdateUser 
    {  
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNo { get; set; }
        public string? Bio { get; set; }
        public string? ProfilePictureUrl { get; set; }

    }
}
