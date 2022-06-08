namespace AstroBhaskar.API.Dto.Response.GlobalResponse
{
    public class ExceptionResponse
    {
        public int StatusCode { get; set; }
        public ErrorResponse ErrorResponse { get; set; }
    }
}
