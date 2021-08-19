using System;
namespace QuizManagerApi.Domain.Models
{
    public class AnswerWithIsCorrect
    {
        public string Answer { get; set; }
        public bool IsCorrect { get; set; }
    }
}
