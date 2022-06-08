using System.Text.Json.Serialization;

namespace AstroBhaskar.API.Dto.BaseDto
{
    public class BaseErrorResponseDto
    {
        public string ErrorResponseType { get; set; }
        public string Message { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public object Errors { get; set; }
    }
}