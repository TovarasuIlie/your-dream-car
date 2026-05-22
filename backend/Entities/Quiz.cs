using System.ComponentModel.DataAnnotations;

namespace backend.Entities
{
    public class Quiz
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }
        public User? User { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public bool IsActive { get; set; } = true;

        public List<QuizAnswer> Answers { get; set; } = new List<QuizAnswer>();
    }
}
