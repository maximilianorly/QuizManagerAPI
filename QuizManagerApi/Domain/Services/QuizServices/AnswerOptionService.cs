using System;
using Microsoft.AspNetCore.Http;
using QuizManagerApi.Domain.Connections;
using System.Collections.Generic;
using QuizManagerApi.Domain.Models.AnswerOption;

namespace QuizManagerApi.Domain.Services
{
    public class AnswerOptionService
    {
        public HttpContext HttpContext { get; }

        public AnswerOptionService(HttpContext httpContext)
        {
            HttpContext = httpContext;
        }

        public IEnumerable<AnswerOption> GetAllAnswers()
        {
            AnswerOptionConnection _answerOptionConnection = HttpContext.RequestServices.GetService(typeof(QuizManagerApi.Domain.Connections.AnswerOptionConnection)) as AnswerOptionConnection;

            var _answerOptions = _answerOptionConnection.GetAllAnswerOptions();

            return _answerOptions;
        }

        public List<AnswerOption> GetAllAnswerOptionsForQuestion(int QuestionId)
        {
            AnswerOptionConnection _answerOptionConnection = HttpContext.RequestServices.GetService(typeof(QuizManagerApi.Domain.Connections.AnswerOptionConnection)) as AnswerOptionConnection;

            var _answerOptions = _answerOptionConnection.GetAllAnswerOptionsForQuestion(QuestionId);

            return _answerOptions;
        }

        public AnswerOption CreateNewAnswerOption(AnswerOption Option)
        {
            AnswerOptionConnection _answerOptionConnection = HttpContext.RequestServices.GetService(typeof(QuizManagerApi.Domain.Connections.AnswerOptionConnection)) as AnswerOptionConnection;

            var _answerOption = _answerOptionConnection.CreateNewAnswerOption(Option);

            return _answerOption;
        }

        public bool IsNewAnswerOptionCreationSuccessful(int AnswerOptionId)
        {
            AnswerOptionConnection _answerOptionConnection = HttpContext.RequestServices.GetService(typeof(QuizManagerApi.Domain.Connections.AnswerOptionConnection)) as AnswerOptionConnection;

            bool _isExistingAnswer = _answerOptionConnection.isNewAnswerOptionCreationSuccessful(AnswerOptionId);

            return _isExistingAnswer;
        }
    }
}
