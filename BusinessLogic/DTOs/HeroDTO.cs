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
        public string Name { get; set; }
    }
}
