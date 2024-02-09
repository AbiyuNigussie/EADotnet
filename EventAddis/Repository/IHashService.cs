using Microsoft.AspNetCore.Identity;

namespace EventAddis.Repository
{
    public interface IHashService
    {
        Dictionary<string, string> HashPassword(string password);
        PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string password);
    }
}
