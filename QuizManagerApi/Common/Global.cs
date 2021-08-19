using System;
using System.Collections.Generic;
using QuizManagerApi.Domain.Models;

namespace QuizManagerApi.Common
{
    public static class Global
    {
        public static List<User> Users { get; set; } = new List<User>();
        public static List<QuizHasQuestionsAndAnswers> Quizzes { get; set; } = new List<QuizHasQuestionsAndAnswers>();
    }
}
