using System;
using System.Collections.Generic;

namespace QuizManagerApi.Domain.Models
{
    public class QuestionHasAnswers
    {
        public int? QuestionId { get; set; }
        public string Question { get; set; }
        public List<AnswerWithIsCorrect> Answers { get; set; }
    }
}
