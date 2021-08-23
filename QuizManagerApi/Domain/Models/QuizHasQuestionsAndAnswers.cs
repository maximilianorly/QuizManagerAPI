using System;
using System.Collections.Generic;
using QuizManagerApi.Domain.Models;

namespace QuizManagerApi.Domain.Models
{
    public class QuizHasQuestionsAndAnswers
    {
        public string QuizName { get; set; }
        public List<QuestionHasAnswers> QuestionsWithAnswers { get; set; }
    }
}
