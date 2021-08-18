using System;
using QuizManagerApi.Domain.Connections;
using System.Collections.Generic;
using QuizManagerApi.Domain.Models;
using MySql.Data.MySqlClient;
using System.Linq;

namespace QuizManagerApi.Domain.Services
{
    public class QuizService
    {
        //public QuizService()
        //{

        //}

        private readonly QuizConnection _quizConnection;

        public QuizService(MySqlConnection conn)
        {
            _quizConnection = new QuizConnection(conn);
        }


        public IEnumerable<Quiz> GetAllActiveQuizzes()
        {
            IEnumerable<Quiz> _quizzes = _quizConnection.GetAllActiveQuizzes();

            return _quizzes.AsEnumerable();
        }

        //public IEnumerable<Quiz> GetQuizQuestionsByQuizId()
        //{
        //    IEnumerable<Question> _questions = 
        //}
    }
}
