using System.ComponentModel.DataAnnotations;

namespace EventAddis.Models
{
    public class UserCredential
    {
        [Key]
        public Guid CredentialId { get; set; }
        public Guid UserId { get; set; }
        public string HashedPassword { get; set; } = null!;
        public string Salt { get; set; } = null!;
        public UserInfo UserInfo { get; set; }

    }
}
