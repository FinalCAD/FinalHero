using Newtonsoft.Json;
using System.Collections.Generic;

namespace BusinessLogic.DTO.Responses
{
    /// <summary>
    /// DTO for list of cities
    /// </summary>
    public class CitiesResponseDTO
    {
        [JsonProperty("cities")]
        public List<CityDTO> Entities { get; set; } 
    }
}
