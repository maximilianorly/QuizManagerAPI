using System;
using Microsoft.AspNetCore.Http;
using QuizManagerApi.Domain.Connections;
using System.Collections.Generic;
using QuizManagerApi.Domain.Models;
using MySql.Data.MySqlClient;

namespace QuizManagerApi.Domain.Services
{
    public class QuestionService
    {
        //public HttpContext HttpContext { get; }

        //public QuestionService(HttpContext httpContext)
        //{
        //    HttpContext = httpContext;
        //}

        private readonly QuestionConnection _questionConnection;

        public QuestionService(MySqlConnection conn)
        {
            _questionConnection = new QuestionConnection(conn);
        }

        public IEnumerable<QuizQuestion> GetAllQuestions()
        {
            //QuestionConnection _questionConnection = HttpContext.RequestServices.GetService(typeof(QuizManagerApi.Domain.Connections.QuestionConnection)) as QuestionConnection;

            var _quizQuestions = _questionConnection.GetAllQuizQuestions();

            return _quizQuestions;
        }

        public IEnumerable<QuizQuestion> GetQuizQuestionsByQuizId(int QuizId)
        {
            IEnumerable<QuizQuestion> _questions = _questionConnection.GetQuizQuestionsByQuizId(QuizId);

            return _questions;
        }

        public IEnumerable<QuizQuestion> GetAllActiveQuestions()
        {
            //QuestionConnection _questionConnection = HttpContext.RequestServices.GetService(typeof(QuizManagerApi.Domain.Connections.QuestionConnection)) as QuestionConnection;

            var _quizQuestions = _questionConnection.GetAllActiveQuizQuestions();

            return _quizQuestions;
        }

        public QuizQuestion GetQuestionById(int Id)
        {
            //QuestionConnection _questionConnection = HttpContext.RequestServices.GetService(typeof(QuizManagerApi.Domain.Connections.QuestionConnection)) as QuestionConnection;

            var _quizQuestion = _questionConnection.GetQuizQuestionById(Id);

            return _quizQuestion;
        }

        public QuizQuestion CreateNewQuestion(QuizQuestion Question)
        {
            //QuestionConnection _questionConnection = HttpContext.RequestServices.GetService(typeof(QuizManagerApi.Domain.Connections.QuestionConnection)) as QuestionConnection;

            var _quizQuestion = _questionConnection.CreateNewQuizQuestion(Question);

            return _quizQuestion;
        }

        public bool IsNewQuestionCreationSuccessful(int QuestionId)
        {
            //QuestionConnection _questionConnection = HttpContext.RequestServices.GetService(typeof(QuizManagerApi.Domain.Connections.QuestionConnection)) as QuestionConnection;

            var _quizQuestion = _questionConnection.isNewQuestionCreationSuccessful(QuestionId);

            return _quizQuestion;
        }
    }
}
