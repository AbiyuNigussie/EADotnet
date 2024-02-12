using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebService.API.Entity;

namespace EventAddis.Entity
{
    public class UserCredential
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CredentialId {get;set;}
        public Guid UserId { get;set;}  
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public UserInfo User { get; set; }
    }
}
