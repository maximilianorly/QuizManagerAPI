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
        private readonly QuestionConnection _questionConnection;

        public QuestionService(MySqlConnection conn)
        {
            _questionConnection = new QuestionConnection(conn);
        }

        public IEnumerable<QuizQuestion> GetAllQuestions()
        {
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
            var _quizQuestions = _questionConnection.GetAllActiveQuizQuestions();

            return _quizQuestions;
        }

        public QuizQuestion GetQuestionById(int Id)
        {
            var _quizQuestion = _questionConnection.GetQuizQuestionById(Id);

            return _quizQuestion;
        }

        public QuizQuestion CreateNewQuestion(QuizQuestion Question)
        {
            var _quizQuestion = _questionConnection.CreateNewQuizQuestion(Question);

            return _quizQuestion;
        }

        public bool IsNewQuestionCreationSuccessful(int QuestionId)
        {
            var _quizQuestion = _questionConnection.isNewQuestionCreationSuccessful(QuestionId);

            return _quizQuestion;
        }
    }
}
