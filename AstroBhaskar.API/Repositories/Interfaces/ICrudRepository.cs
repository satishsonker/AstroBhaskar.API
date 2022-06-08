using AstroBhaskar.API.Dto.BaseDto;
using System.Threading.Tasks;

namespace AstroBhaskar.API.Repositories.Interfaces
{
    public interface ICrudRepository<T> where T : class
    {
        Task<BasePagination<T>> GetAll(int pageNo, int pageSize);
        Task<T> Get(int id);
        Task<int> Add(T entity);
        Task<int> Update(T entity);
        Task<int> Delete(int id);
        Task<BasePagination<T>> Search(string searchTerm, int pageNo, int pageSize);

    }
}
