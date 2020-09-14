using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BusinessLogic.DTOs
{
    public class HeroDTO
    {

        [Required,MaxLength(40,
            ErrorMessage ="that a hero names, keep it short and beauti and no more than fourthy !!")]
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}