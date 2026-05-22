using backend.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace backend.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        public List<Quiz> Quizzes { get; set; } = new List<Quiz>();
        public UserRole Role { get; set; }
    }
}
