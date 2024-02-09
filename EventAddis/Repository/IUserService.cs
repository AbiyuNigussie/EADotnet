using EventAddis.Dto;
using EventAddis.Models;

namespace EventAddis.Repositories
{
    public interface IUserService
    {
        Task<ICollection<UserInfo>> GetUserInfos();
        Task<bool> UserInfoExist(Guid id);
        Task<bool> CreateUser(UserDetailsDto user);
        
        Task<bool> Save();
    }
}
