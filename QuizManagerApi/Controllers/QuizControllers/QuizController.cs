using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using QuizManagerApi.Common;
using QuizManagerApi.Domain.Models;
using QuizManagerApi.Domain.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QuizManagerApi.Controllers.QuizControllers
{
    [Route("api/[controller]")]
    public class QuizController : Controller
    {

        public QuizService _quizService;

        public QuizController(MySqlConnection conn)
        {
            _quizService = new QuizService(conn);
        }

        // GET: api/Quiz
        [HttpGet]
        public IEnumerable<Quiz> GetAllQuizzes()
        {
            IEnumerable<Quiz> _quizzes = _quizService.GetAllQuizzes();

            return _quizzes;
        }

        //GET api/Quiz/Active
        [HttpGet("Active")]
        public IEnumerable<Quiz> GetAllActiveQuizzes()
        {
            IEnumerable<Quiz> _quizzes = _quizService.GetAllActiveQuizzes();

            return _quizzes;
        }

        // GET: api/Quiz/5
        [HttpGet("{QuizId}")]
        public IEnumerable<QuizQuestion> GetQuizQuestionsByQuizId(int QuizId)
        {
            IEnumerable<QuizQuestion> _questions = _quizService.GetQuizQuestionsByQuizId(QuizId);

            return _questions;
        }

        // POST api/Quiz
        [HttpPost]
        public Quiz Post([FromBody] QuizHasQuestionsAndAnswers newQuiz)
        {
            Quiz _newQuiz = _quizService.CreateNewQuiz(newQuiz);

            return _newQuiz;
        }

        // PUT api/Quiz/5
        [HttpPut("updateQuestion/{QuizId}/{UserId}")]
        public IEnumerable<QuizQuestion> Put(int QuizId, int UserId, [FromBody] QuestionHasAnswers NewQuestionWithAnswers)
        {
            IEnumerable<QuizQuestion> _updatedQuizQuestion = _quizService.UpdateQuiz(QuizId, UserId, NewQuestionWithAnswers, NewQuestionWithAnswers.QuestionId);

            return _updatedQuizQuestion;
        }

        // PUT api/Quiz/5/true
        [HttpPut("{QuizId}/{IsActive}")]
        public Quiz Put(int QuizId, bool IsActive)
        {
            Quiz _quiz = _quizService.UpdateQuizSetIsActive(QuizId, IsActive);

            return _quiz;
        }
    }
}
