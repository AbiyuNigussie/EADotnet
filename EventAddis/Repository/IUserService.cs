using WebService.API.Entity;
using WebService.API.Models;

namespace WebService.API.Repository
{
    public interface IUserService
    {
        IEnumerable<UserInfo> GetUsers();
        UserInfo GetUserbyId(Guid id);
        void PutUser(Guid id, UpdateUser user);
        UserInfo PostUser(RegisterUser create, string Password);
        void DeleteUser(UserInfo user);
        public bool IsExist(Guid id);
        
    }
}
