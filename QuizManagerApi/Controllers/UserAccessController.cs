using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using QuizManagerApi.Domain.Connections;
using QuizManagerApi.Domain.Models.UserHasAccess;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QuizManagerApi.Controllers
{
    [Route("api/[controller]")]
    public class UserAccessController : Controller
    {
        // GET: api/UserAccess
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/UserAccess/5
        [HttpGet("{UserId}")]
        public UserHasAccess Get(int UserId)
        {
            UserAccessConnection context = HttpContext.RequestServices.GetService(typeof(QuizManagerApi.Domain.Connections.UserAccessConnection)) as UserAccessConnection;

            var data = context.GetUserAccessByUserId(UserId);
            Console.WriteLine(data);
            return data;
        }

        // POST api/UserAccess
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/UserAccess/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/UserAccess/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
