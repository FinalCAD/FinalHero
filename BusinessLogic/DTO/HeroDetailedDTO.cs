using Newtonsoft.Json;
using System.Collections.Generic;

namespace BusinessLogic.DTO
{
    /// <summary>
    /// DTO for Hero containing all complete informations
    /// </summary>
    public class HeroDetailedDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("city")]
        public CityDTO City { get; set; }

        [JsonProperty("powers")]
        public List<PowerDTO> Powers { get; set; }
    }
}
