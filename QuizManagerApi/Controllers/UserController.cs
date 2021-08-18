using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuizManagerApi.Domain.Connections;
using QuizManagerApi.Domain.Services;
using QuizManagerApi.Domain.Models.User;
using QuizManagerApi.Domain.Models.LogInCredentials;
using Microsoft.AspNetCore.Mvc;
using QuizManagerApi.Domain.IService;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QuizManagerApi.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {

        // GET: api/user
        [HttpGet]
        public IEnumerable<User> Get()
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

        // POST api/user/Login
        [HttpPost("Login")]
        public User Login([FromBody] LogInCredentials oUser)
        {
            UserService _userService = new UserService(HttpContext);

            User _user = _userService.Login(oUser);

            return _user;
        }

        // POST api/user/Signup
        [HttpPost("Signup")]
        public User SignUp([FromBody] User oUser)
        {
            UserService _userService = new UserService(HttpContext);

            User _user = _userService.SignUp(oUser);

            return _user;
        }

        // POST api/user
        [HttpPost]
        public User Post([FromBody] LogInCredentials SuppliedCredentials )
        {
            UserService _userService = new UserService(HttpContext);

            var _user = _userService.GetUserByUsername(SuppliedCredentials.Username);

            if (_user == null)
            {
                return null;
            }
            else
            {
                var actualCredentials = new LogInCredentials();

                actualCredentials.Username = _user.UserName;
                actualCredentials.Password = _user.Password;


                //bool _isAuthenticCredentials = _userService.ValidateCredentials(SuppliedCredentials, actualCredentials);
                bool _isAuthenticCredentials = false;

                if (!_isAuthenticCredentials)
                {
                    return null;
                }

                return _user;
            }
        }

        // POST api/user/PostNewUser
        [HttpPost]
        [Route("[action]")]
        public IEnumerable<User> PostNewUser([FromBody] User NewUser, int AccessLevel)
        {
            UserService _userService = new UserService(HttpContext);
            AccessLevelService _accessLevelService = new AccessLevelService(HttpContext);

            if (!_userService.IsExistingUser(NewUser.UserName))
            {
                User _newUser = _userService.CreateUser(NewUser);
                _userService.MapNewUserToAccessLevel(_newUser.Id, AccessLevel);
            }
            
                return _userService.GetAllUsers();

        }

        // PUT api/User/Logout/5
        [HttpPut("Logout/{UserId}")]
        public User Logout(int UserId)
        {
            UserService _userService = new UserService(HttpContext);

            return _userService.Logout(UserId);

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
