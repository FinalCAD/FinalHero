using Newtonsoft.Json;

namespace BusinessLogic.DTO
{
    /// <summary>
    /// DTO for City
    /// </summary>
    public class CityDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }
    }
}
