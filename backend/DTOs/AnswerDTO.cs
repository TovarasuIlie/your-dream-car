namespace backend.DTOs
{
    public class AnswerDTO
    {
        public int Question { get; set; }
        public List<int> Options { get; set; } = new List<int>();
        public int Weight { get; set; }
    }
}
