using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace BusinessLogic.DTOs
{
    /// <summary>
    /// city list
    /// </summary>
    public class CityDTO
    {
        [Required, MaxLength(40, ErrorMessage = "City name have to be less than 40")]
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("heroes")]
        public List<HeroDTO> Heroes { get; set; }
    }
}
