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
        [JsonPropertyName("hero_id")]
        public int HeroId { get; set; }

        [Required]
        [JsonPropertyName("hero")
            , MaxLength(40, ErrorMessage = ("Hero length is Less than 40"))]
        public HeroDTO Hero { get; set; }

        [Required]
        [JsonPropertyName("hero_id")]
        public int PowerId { get; set; }

        [Required]
        [JsonPropertyName("power")
            ,MaxLength(40,ErrorMessage =("Power length is Less than 40"))]
        public PowerDTO Power { get; set; }

    }
}
