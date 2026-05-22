using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace backend.Entities
{
    public class QuizAnswer
    {
        [Key]
        public int Id { get; set; }

        public int QuizId { get; set; }
        [JsonIgnore]
        public Quiz? Quiz { get; set; }

        public int QuizQuestionId { get; set; }
        public QuizQuestion? Question { get; set; }

        public int QuizOptionId { get; set; }
        public QuizOption? SelectedOption { get; set; }
        public int Weight { get; set; }
    }
}
