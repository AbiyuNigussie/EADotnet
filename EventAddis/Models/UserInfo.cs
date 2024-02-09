using System.ComponentModel.DataAnnotations;

namespace EventAddis.Models
{
    public class UserInfo
    {
        [Key]
        public Guid UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Mobile { get; set; }
        public string Email { get; set; }
        public string? Gender { get; set; }
        public string? Bio { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public DateTime RegisteredAt { get; set; }

        public DateTime LastLogin { get; set; }

        public bool? Active { get; set; }
        public DateTime? DeactivationDate { get; set; }

        public UserCredential UserCredential { get; set; }

    }
}
