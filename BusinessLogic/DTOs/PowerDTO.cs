using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace BusinessLogic.DTOs
{
    public class PowerDTO
    {
        
        [JsonPropertyName("id")]
        public int Id { get; set; }


        [JsonPropertyName("power")]
        public string Name { get; set; }

    }
}
