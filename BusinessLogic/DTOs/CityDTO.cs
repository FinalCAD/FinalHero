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
        [Required]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [Required, MaxLength(40, ErrorMessage = "City name have to be less than 40")]
        [JsonPropertyName("city_name")]
        public string Name { get; set; }

        [JsonPropertyName("heroes")]
        public virtual List<HeroDTO> Heroes { get; set; }
    }
}
