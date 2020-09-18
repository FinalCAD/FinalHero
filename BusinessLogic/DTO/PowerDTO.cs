using Newtonsoft.Json;

namespace BusinessLogic.DTO
{
    /// <summary>
    /// DTO for Power
    /// </summary>
    public class PowerDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string? Description { get; set; }
    }
}
