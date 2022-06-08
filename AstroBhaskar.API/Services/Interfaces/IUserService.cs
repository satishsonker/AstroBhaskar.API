using AstroBhaskar.API.Dto.Request;
using AstroBhaskar.API.Dto.Response;
using System.Threading.Tasks;

namespace AstroBhaskar.API.Services.Interfaces
{
    public interface IUserService : ICrudService<UserRequest, UserResponse>
    {
        Task<int> ResetPassword(string email);
        Task<int> ToggleLockUser(string email, bool isLocked);
        Task<int> ToggleBlockUser(string email, bool isBlocked);
    }
}
