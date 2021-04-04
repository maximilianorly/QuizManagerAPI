using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuizManagerApi.Domain.Services;
using QuizManagerApi.Domain.Models.QuizQuestion;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QuizManagerApi.Controllers.Quiz
{
    [Route("api/[controller]")]
    public class QuestionsController : Controller
    {
        // GET: api/Questions
        [HttpGet]
        public IEnumerable<QuizQuestion> Get()
        {
            QuestionService _questionService = new QuestionService(HttpContext);

            var _questions = _questionService.GetAllQuestions();
            return _questions;
        }

        // GET api/Questions/5
        [HttpGet("{Id}")]
        public QuizQuestion Get(int Id)
        {
            QuestionService _questionService = new QuestionService(HttpContext);

            var _question = _questionService.GetQuestionById(Id);
            return _question;
        }

        // POST api/Questions
        // IsActive Must be supplied as boolean, not 0 or 1 in postman??
        [HttpPost]
        public bool Post([FromBody] QuizQuestion Question)
        {
            QuestionService _questionService = new QuestionService(HttpContext);

            var _newQuestion = _questionService.CreateNewQuestion(Question);
            bool _isNewQuestionCreationSuccessful = _questionService.IsNewQuestionCreationSuccessful(_newQuestion.Id);

            return _isNewQuestionCreationSuccessful;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
