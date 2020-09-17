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


        [JsonPropertyName("id")]
        [Required]
        public int Id { get; set; }


        [JsonPropertyName("name")]
        [Required, MaxLength(40,
            ErrorMessage = "that a hero names, keep it short and beauti and no more than fourthy !!")]
        public string Name { get; set; }


        [Required]
        [JsonPropertyName("city")]
        public CityDTO CityDTO { get; set; }


        [JsonPropertyName("powers")]
        [Required]
        public virtual List<HeroPowerDTO> HeroPowerDTOs { get; set; }

        [Required,DefaultValue(0)]
        [JsonPropertyName("powers_count")]
        public int PowersCount { get; set; }
    }
}