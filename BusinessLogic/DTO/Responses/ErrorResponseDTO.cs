using Newtonsoft.Json;

namespace BusinessLogic.DTO.Responses
{
    /// <summary>
    /// Response DTO for error
    /// </summary>
    public class ErrorResponseDTO
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
