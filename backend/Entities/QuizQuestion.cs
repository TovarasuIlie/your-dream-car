using Microsoft.VisualBasic.FileIO;
using System.ComponentModel.DataAnnotations;

namespace backend.Entities
{
    public class QuizQuestion
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Subtitle { get; set; } = string.Empty;

        public List<QuizOption> Options { get; set; } = new List<QuizOption>();
    }
}
