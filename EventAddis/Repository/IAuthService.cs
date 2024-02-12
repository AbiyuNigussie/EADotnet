using Microsoft.AspNetCore.Identity;
using WebService.API.Entity;
using WebService.API.Models;

namespace WebService.API.Repository
{
    public interface IAuthService
    {
        (PasswordVerificationResult passwordVerificationResult, UserInfo userDetails) Authenticate(AuthUser auth);

        string Generate(UserInfo user);
    }
}
