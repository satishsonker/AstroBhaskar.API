using AstroBhaskar.API.Dto.Request;
using AstroBhaskar.API.Dto.Response;
using System.Threading.Tasks;

namespace AstroBhaskar.API.Services.Interfaces
{
    public interface IMasterCollectionService : ICrudService<MasterCollectionRequest, MasterCollectionResponse>
    {
        Task<PagingResponse<MasterCollectionResponse>> Get(string collectionName, int pageNo, int pageSize);
        Task<int> Delete(string collectionName);
        Task<int> Delete(string collectionName, string key);
    }
}
