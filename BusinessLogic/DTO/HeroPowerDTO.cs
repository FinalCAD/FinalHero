using Newtonsoft.Json;

namespace BusinessLogic.DTO
{
    /// <summary>
    /// DTO for HeroPower
    /// </summary>
    public class HeroPowerDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("heroId")]
        public int HeroId { get; set; }

        [JsonProperty("powerId")]
        public int PowerId { get; set; }
    }
}
