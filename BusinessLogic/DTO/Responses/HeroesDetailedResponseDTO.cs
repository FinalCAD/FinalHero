using Newtonsoft.Json;
using System.Collections.Generic;

namespace BusinessLogic.DTO.Responses
{
    /// <summary>
    /// DTO for list of detailed heroes
    /// </summary>
    public class HeroesDetailedResponseDTO
    {
        [JsonProperty("heroes")]
        public List<HeroDetailedDTO> Entities { get; set; }
    }
}
