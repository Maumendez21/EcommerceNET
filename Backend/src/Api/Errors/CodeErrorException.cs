using Newtonsoft.Json;

namespace Api.Errors
{
    public class CodeErrorException : CodeErrorResponse
    {
        [JsonProperty(PropertyName = "Details")]
        public string? Details { get; set; }
        public CodeErrorException(int statusCode, string[]? message = null, string? details = null) : base(statusCode, message)
        {
            Details = details;
        }
    }
}