using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace backend.Entities
{
    public class QuizOption
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Label { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Required]
        public int QuizQuestionId { get; set; }

        [JsonIgnore]
        public QuizQuestion? QuizQuestion { get; set; }
    }
}
