using System.Threading.Tasks;

namespace AstroBhaskar.API.Repositories.Interfaces
{
    public interface ICommonRepository
    {
        Task<bool> IsUserExist(string email);
        Task<bool> IsUserExist(int userId);
        Task<bool> IsPermissionExist(int userId);
        Task<bool> IsMasterCollectionExist(int collectionId);
        Task<bool> IsMasterCollectionExist(string collectionName);
        Task<bool> IsMasterCollectionExist(string collectionName, string key);

    }
}