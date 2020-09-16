using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace BusinessLogic.DTOs
{
    public class HeroDTO
    {

        [JsonPropertyName("id")]
        [Required]
        public int Id { get; set; }


        [JsonPropertyName("name")]
        [Required, MaxLength(40,
         ErrorMessage = "that a hero names, keep it short and beauti and no more than fourthy !!")]
        public string HeroName { get; set; }


        [JsonPropertyName("city_id")]
        [Required]
        public int CityId { get; set; }

        [JsonPropertyName("city")]
        [MaxLength(40,
         ErrorMessage = "that a hero names, keep it short and beauti and no more than fourthy !!")]
        public string CityName { get; set; }


        //[JsonPropertyName("powers")]
        //public virtual List<PowerDTO> Powers { get; set; }

        //[JsonPropertyName("powers_count")]
        //public int PowersCount { get; set; } = 0;


    }
}
