using System;
using Microsoft.AspNetCore.Http;
using QuizManagerApi.Domain.Connections;
using System.Collections.Generic;
using QuizManagerApi.Domain.Models;
using MySql.Data.MySqlClient;

namespace QuizManagerApi.Domain.Services
{
    public class AnswerOptionService
    {

        private readonly AnswerOptionConnection _answerOptionConnection;

        public AnswerOptionService(MySqlConnection conn)
        {
            _answerOptionConnection = new AnswerOptionConnection(conn);
        }

        public IEnumerable<AnswerOption> GetAllAnswers()
        {
            var _answerOptions = _answerOptionConnection.GetAllAnswerOptions();

            return _answerOptions;
        }

        public List<AnswerOption> GetAllAnswerOptionsForQuestion(int QuestionId)
        {
            var _answerOptions = _answerOptionConnection.GetAllAnswerOptionsForQuestion(QuestionId);

            return _answerOptions;
        }

        public AnswerOption CreateNewAnswerOption(AnswerOption Option)
        {
            var _answerOption = _answerOptionConnection.CreateNewAnswerOption(Option);

            return _answerOption;
        }

        public bool IsNewAnswerOptionCreationSuccessful(int AnswerOptionId)
        {
            bool _isExistingAnswer = _answerOptionConnection.isNewAnswerOptionCreationSuccessful(AnswerOptionId);

            return _isExistingAnswer;
        }
    }
}
