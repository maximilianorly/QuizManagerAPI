namespace QuizManagerApi.Domain.Models.AnswerOption
{
    public class AnswerOption
    {
        public int Id { get; set; }
        public string QuestionId { get; set; }
        public bool IsCorrectOption { get; set; }
        public string Option { get; set; }
        public string Created { get; set; }
        public string Modified { get; set; }
    }
}
