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


        [JsonPropertyName("hero_id")]
        public int HeroID { get; set; }


        [JsonPropertyName("hero_power")]
        public virtual HeroPower HeroPower { get; set; }


        [JsonPropertyName("power_id")]
        public int PowerId { get; set; }

    }
}
