using Newtonsoft.Json;

namespace BusinessLogic.DTO
{
    /// <summary>
    /// DTO for Hero
    /// </summary>
    public class HeroDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("cityId")]
        public int? CityId { get; set; }
    }
}
