using AstroBhaskar.API.Dto.BaseDto;
using AstroBhaskar.API.Models;
using System.Threading.Tasks;

namespace AstroBhaskar.API.Repositories.Interfaces
{
    public interface IMasterCollectionRepository : ICrudRepository<MasterCollection>
    {
        Task<BasePagination<MasterCollection>> Get(string collectionName, int pageNo, int pageSize);
        Task<int> Delete(string collectionName);
        Task<int> Delete(string collectionName, string key);
    }
}
