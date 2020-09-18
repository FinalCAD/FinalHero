using Newtonsoft.Json;
using System.Collections.Generic;

namespace BusinessLogic.DTO.Responses
{
    /// <summary>
    /// DTO for list of heroes powers
    /// </summary>
    public class HeroesPowersResponseDTO
    {
        [JsonProperty("heroespowers")]
        public List<HeroPowerDTO> Entities { get; set; }
    }
}
