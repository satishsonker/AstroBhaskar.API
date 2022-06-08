using AstroBhaskar.API.Models;
using System.Threading.Tasks;

namespace AstroBhaskar.API.Repositories.Interfaces
{
    public interface IUserRepository : ICrudRepository<AstroUser>
    {
        Task<int> ResetPassword(string email, string resetCode);
        Task<int> ToggleLockUser(string email, bool isLocked);
        Task<int> ToggleBlockUser(string email, bool isBlocked);
    }
}
