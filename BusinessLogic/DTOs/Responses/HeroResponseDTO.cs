using BusinessLogic.DTOs;
using BusinessLogic.DTOs.Responses;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BusinessLogic.Services.Interfaces
{


    public class HeroResponseDTO : DifferentialDTO<HeroCityPowersDTO>
    {

        [JsonPropertyName("entities")]
        public override List<HeroCityPowersDTO> Entities { get; set; }

    }
}