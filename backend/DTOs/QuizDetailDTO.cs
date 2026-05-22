namespace backend.DTOs
{
    public class QuizDetailDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
        public List<QuizDetailAnswerDTO> Answers { get; set; } = new List<QuizDetailAnswerDTO>();
    }
}
