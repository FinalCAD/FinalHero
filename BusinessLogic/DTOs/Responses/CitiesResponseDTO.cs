using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace BusinessLogic.DTOs.Responses
{
    /// <summary>
    /// a City with 
    /// </summary>
    public class CityResponseDTO : DifferentialDTO<CityDTO>
    {
        [JsonPropertyName("cities")]
        public override List<CityDTO> Entities { get; set; }
    }
}
