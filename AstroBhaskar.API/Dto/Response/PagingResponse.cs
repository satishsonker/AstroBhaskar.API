using AstroBhaskar.API.Dto.BaseDto;

namespace AstroBhaskar.API.Dto.Response
{
    public class PagingResponse<T> : BasePagination<T> where T : class
    {
    }
}
