namespace QuizManagerApi.Domain.Models
{ 

    public class AnswerOption
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public bool IsCorrectOption { get; set; }
        public string Option { get; set; }
        public string Created { get; set; }
        public string Modified { get; set; }
    }
}
