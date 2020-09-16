using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BusinessLogic.DTOs
{
    /// <summary>
    /// UC He1 :
    /// </summary>
    public class HeroCityPowersDTO
    {


        [JsonPropertyName("hero_id")]
        [Required]
        public int Id { get; set; }


        [JsonPropertyName("hero_name")]
        [Required, MaxLength(40,
            ErrorMessage = "that a hero names, keep it short and beauti and no more than fourthy !!")]
        public string HeroName { get; set; }

        
        [JsonPropertyName("city_id")]
        public int CityId { get; set; }


        [JsonPropertyName("city_name")]
        [Required, MaxLength(30,
            ErrorMessage = "City Name must be less than 30")]
        public string CityName { get; set; }


        [JsonPropertyName("powers")]
        [Required]
        public List<PowerDTO> Powers { get; set; }

        [Required,DefaultValue(0)]
        [JsonPropertyName("powers_count")]
        public int PowersCount { get; set; }
    }
}