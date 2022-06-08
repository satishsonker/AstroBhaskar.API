using System.Collections.Generic;

namespace AstroBhaskar.API.Dto.BaseDto
{
    public class BasePagination<T> where T : class
    {
        public int TotalRecords { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public List<T> Data { get; set; }
    }
}
