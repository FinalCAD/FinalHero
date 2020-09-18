using Newtonsoft.Json;
using System.Collections.Generic;

namespace BusinessLogic.DTO.Responses
{
    /// <summary>
    /// DTO for list of heroes
    /// </summary>
    public class HeroesResponseDTO
    {
        [JsonProperty("heroes")]
        public List<HeroDTO> Entities { get; set; }
    }
}
