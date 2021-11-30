using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuizManagerApi.Domain.Connections;
using QuizManagerApi.Domain.Services;
using QuizManagerApi.Domain.Models;
using QuizManagerApi.Domain.Models.LogInCredentials;
using Microsoft.AspNetCore.Mvc;
using QuizManagerApi.Domain.IService;
using MySql.Data.MySqlClient;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QuizManagerApi.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {

        public UserService _userService;
        public AccessLevelService _accessLevelService;

        public UserController(MySqlConnection conn)
        {
            _userService = new UserService(conn);
            _accessLevelService = new AccessLevelService(conn);
        }

        // GET: api/user
        [HttpGet]
        public IEnumerable<User> Get()
        {

            var _users = _userService.GetAllUsers();
            return _users; 
        }

        // GET api/user/5
        [HttpGet("{Id}")]
        public User Get(int Id)
        {

            var _user = _userService.GetUserById(Id);
            return _user;
        }

        // POST api/user/Login
        [HttpPost("Login")]
        public UserHasAccess Login([FromBody] LogInCredentials oUser)
        {

            UserHasAccess _user = _userService.Login(oUser);

            return _user;
        }

        // POST api/user/Signup/5
        [HttpPost("Signup/{AccessLevelId}")]
        public User SignUp(int AccessLevelId, [FromBody] User oUser)
        {

            User _user = _userService.SignUp(oUser, AccessLevelId);

            return _user;
        }

        // POST api/user
        [HttpPost]
        public User Post([FromBody] LogInCredentials SuppliedCredentials )
        {

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
            return _userService.Logout(UserId);
        }
    }
}
