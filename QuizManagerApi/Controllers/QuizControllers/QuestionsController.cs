using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuizManagerApi.Domain.Services;
using QuizManagerApi.Domain.Models;
using MySql.Data.MySqlClient;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QuizManagerApi.Controllers.QuizControllers
{
    [Route("api/[controller]")]
    public class QuestionsController : Controller
    {

        public QuestionService _questionService;

        public QuestionsController(MySqlConnection conn)
        {
            _questionService = new QuestionService(conn);
        }

        // GET: api/Questions
        [HttpGet]
        public IEnumerable<QuizQuestion> Get()
        {

            IEnumerable<QuizQuestion> _questions = _questionService.GetAllQuestions();
            return _questions;
        }

        // GET api/Questions/5
        [HttpGet("{Id}")]
        public QuizQuestion Get(int Id)
        {
            QuizQuestion _question = _questionService.GetQuestionById(Id);
            return _question;
        }

        [HttpGet("GetAllActiveQuestions")]
        public IEnumerable<QuizQuestion> GetAllActiveQuestions()
        {
            IEnumerable<QuizQuestion> _questions = _questionService.GetAllActiveQuestions();
            return _questions;
        }

        // POST api/Questions
        // IsActive Must be supplied as boolean, not 0 or 1 in postman??
        [HttpPost]
        public bool Post([FromBody] QuizQuestion Question)
        {
            QuizQuestion _newQuestion = _questionService.CreateNewQuestion(Question);
            bool _isNewQuestionCreationSuccessful = _questionService.IsNewQuestionCreationSuccessful(_newQuestion.Id);

            return _isNewQuestionCreationSuccessful;
        }
    }
}
