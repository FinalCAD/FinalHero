using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace BusinessLogic.DTOs
{
    public class HeroPowerDTO
    {

        [Required]
        [JsonPropertyName("id")]
        public int Id { get; set; }
        
        [Required]
        [JsonPropertyName("hero_id")]
        public int HeroId { get; set; }

        [Required]
        [JsonPropertyName("power_id")]
        public int PowerId { get; set; }

    }
}
