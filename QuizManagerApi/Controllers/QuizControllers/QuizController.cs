using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
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
        public IEnumerable<Quiz> GetAllActiveQuizzes()
        {
            IEnumerable<Quiz> _quizzes = _quizService.GetAllActiveQuizzes();

            return _quizzes;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
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
