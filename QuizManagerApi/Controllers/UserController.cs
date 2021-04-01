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
            UsersConnection context = HttpContext.RequestServices.GetService(typeof(QuizManagerApi.Domain.Connections.UsersConnection)) as UsersConnection;

            var data = context.GetAllUsers();
            Console.WriteLine(data);
            return data; 
        }

        // GET api/user/5
        [HttpGet("{id}")]
        public User Get(int id)
        {
            UsersConnection context = HttpContext.RequestServices.GetService(typeof(QuizManagerApi.Domain.Connections.UsersConnection)) as UsersConnection;

            var data = context.GetUserByID(id);
            Console.WriteLine(data);
            return data;
        }

        // POST api/user
        [HttpPost]
        public bool Post([FromBody] LogInCredentials SuppliedCredentials)
        {
            UserService _userService = new UserService();

            UsersConnection _usersConnection = HttpContext.RequestServices.GetService(typeof(QuizManagerApi.Domain.Connections.UsersConnection)) as UsersConnection;

            var user = _usersConnection.GetUserByUsername(SuppliedCredentials.Username);

            var actualCredentials = new LogInCredentials();

            actualCredentials.Username = user.UserName;
            actualCredentials.Password = user.Password;

            // return $"Supplied: {SuppliedCredentials.Username} + {SuppliedCredentials.Password}, Actual: {actualCredentials.Username} + {actualCredentials.Password}";

            return _userService.ValidateCredentials(SuppliedCredentials, actualCredentials);

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
