using Newtonsoft.Json;
using System.Collections.Generic;

namespace BusinessLogic.DTO.Responses
{
    /// <summary>
    /// DTO for list of powers
    /// </summary>
    public class PowersResponseDTO
    {
        [JsonProperty("powers")]
        public List<PowerDTO> Entities { get; set; }
    }
}
