namespace backend.DTOs
{
    public class QuizDetailAnswerDTO
    {
        public string Question { get; set; } = string.Empty;
        public List<string> Responses { get; set; } = new List<string>();
        public int Weight { get; set; }
    }
}
