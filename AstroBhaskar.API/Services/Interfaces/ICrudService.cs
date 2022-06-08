using AstroBhaskar.API.Dto.Response;
using System.Threading.Tasks;

namespace AstroBhaskar.API.Services.Interfaces
{
    public interface ICrudService<in TRequest, TResponse> where TRequest : class where TResponse : class
    {
        Task<PagingResponse<TResponse>> GetAll(int pageNo, int pageSize);
        Task<TResponse> Get(int id);
        Task<int> Add(TRequest entity);
        Task<int> Update(TRequest entity);
        Task<int> Delete(int id);
        Task<PagingResponse<TResponse>> Search(string searchTerm, int pageNo, int pageSize);

    }
}
