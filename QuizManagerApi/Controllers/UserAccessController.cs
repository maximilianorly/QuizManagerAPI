using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using QuizManagerApi.Domain.Connections;
using QuizManagerApi.Domain.Models;
using QuizManagerApi.Domain.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QuizManagerApi.Controllers
{
    [Route("api/[controller]")]
    public class UserAccessController : Controller
    {

        public UserService _userService;

        public UserAccessController(MySqlConnection conn)
        {
            _userService = new UserService(conn);
        }
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
            UserHasAccess _userHasAccess = _userService.GetUserAccessByUserId(UserId);

            return _userHasAccess;
        }

        // POST api/UserAccess
        [HttpPost("{newUserId}/{accessLevelId}")]
        public void Post(int newUserId, int accessLevelId)
        {
            _userService.MapNewUserToAccessLevel(newUserId, accessLevelId);
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
