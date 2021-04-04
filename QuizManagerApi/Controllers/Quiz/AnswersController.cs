﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuizManagerApi.Domain.Services;
using QuizManagerApi.Domain.Models.AnswerOption;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QuizManagerApi.Controllers.Quiz
{
    [Route("api/[controller]")]
    public class AnswersController : Controller
    {
        // GET: api/answers
        [HttpGet]
        public IEnumerable<AnswerOption> Get()
        {
            AnswerOptionService _answerOptionService = new AnswerOptionService(HttpContext);

            var _answers = _answerOptionService.GetAllAnswers();
            return _answers;
        }

        // GET api/answers/5
        [HttpGet("GetAllAnswerOptionsForQuestion/{QuestionId}")]
        public List<AnswerOption> GetAllAnswerOptionsForQuestion(int QuestionId)
        {
            AnswerOptionService _answerOptionService = new AnswerOptionService(HttpContext);

            var _answers = _answerOptionService.GetAllAnswerOptionsForQuestion(QuestionId);
            return _answers;
        }

        // POST api/answers
        [HttpPost]
        public bool Post([FromBody] AnswerOption Option)
        {
            AnswerOptionService _answerOptionService = new AnswerOptionService(HttpContext);

            var _newAnswerOption = _answerOptionService.CreateNewAnswerOption(Option);
            bool _isNewAnswerOptionCreationSuccessful = _answerOptionService.IsNewAnswerOptionCreationSuccessful(_newAnswerOption.Id);

            return _isNewAnswerOptionCreationSuccessful;
        }

        // PUT api/answers/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/answers/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}