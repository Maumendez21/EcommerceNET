using Newtonsoft.Json;

namespace Api.Errors
{
    public class CodeErrorResponse
    {
        [JsonProperty(PropertyName = "statusCode")]
        public int StatusCode { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string[]? Messages { get; set; }
        
        public CodeErrorResponse(int statusCode, string[]? message = null)
        {
            StatusCode = statusCode;
            if (message is null)
            {
                Messages = new string[0];
                Messages[0] = GetDefaultMessageStatusCode(statusCode);
            }
            else
            {
                Messages = message;
            }
        }

        private string GetDefaultMessageStatusCode(int statusCode)
        {
            return statusCode switch 
            {
                400 => "El request enviado tiene errores",
                401 => "No tienes autorización para este recurso",
                404 => "No se encontro el recurso solicitado",
                500 => "Se produjeron erroes en el servidor",
                _ => string.Empty
            };
        }
    }
}