using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuizManagerApi.Domain.Connections;
using QuizManagerApi.Domain.Services;
using QuizManagerApi.Domain.Models.User;
using QuizManagerApi.Domain.Models.LogInCredentials;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QuizManagerApi.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        // GET: api/user
        [HttpGet]
        public IEnumerable<User> Get()
        //public string Get()
        {
            UserService _userService = new UserService(HttpContext);

            var _users = _userService.GetAllUsers();
            return _users; 
        }

        // GET api/user/5
        [HttpGet("{Id}")]
        public User Get(int Id)
        {
            UserService _userService = new UserService(HttpContext);

            var _user = _userService.GetUserById(Id);
            return _user;
        }

        // POST api/user
        [HttpPost]
        public bool Post([FromBody] LogInCredentials SuppliedCredentials )
        {
            UserService _userService = new UserService(HttpContext);

            var _user = _userService.GetUserByUsername(SuppliedCredentials.Username);

            var actualCredentials = new LogInCredentials();

            actualCredentials.Username = _user.UserName;
            actualCredentials.Password = _user.Password;


            return _userService.ValidateCredentials(SuppliedCredentials, actualCredentials);

        }

        // POST api/user/PostNewUser
        [HttpPost]
        [Route("[action]")]
        public IEnumerable<User> PostNewUser([FromBody] User NewUser)
        {
            UserService _userService = new UserService(HttpContext);

            if (!_userService.IsExistingUser(NewUser.UserName))
                return _userService.CreateUser(NewUser);
            else
                return _userService.GetAllUsers();

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
