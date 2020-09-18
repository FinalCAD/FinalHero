using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace BusinessLogic.DTOs
{
    public class CityDTO
    {
        [JsonPropertyName("id")]
        [Required]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        [Required, MaxLength(40, ErrorMessage = "City name have to be less than 40")]
        public string Name { get; set; }

    }
}
