using System;
using Microsoft.AspNetCore.Http;
using QuizManagerApi.Domain.Models.LogInCredentials;
using QuizManagerApi.Domain.Models.User;
using QuizManagerApi.Domain.Models.UserHasAccess;
using QuizManagerApi.Domain.Connections;
using System.Collections.Generic;
using QuizManagerApi.Domain.IService;
using QuizManagerApi.Common;
using System.Linq;

namespace QuizManagerApi.Domain.Services
{
    public class UserService : IUserService
    {
        public HttpContext HttpContext { get; }

        public UserService(HttpContext httpContext)
        {
            HttpContext = httpContext;
        }

        //private UsersConnection UsersConnection { get; }
        //private UserAccessConnection UserAccessConnection { get; }

        //public UserService(UsersConnection usersConnection, UserAccessConnection userAccessConnection)
        //{
        //    UsersConnection = usersConnection;
        //    UserAccessConnection = userAccessConnection;
        //}

        public User Login(User oUser)
        {
            var user = Global.Users.SingleOrDefault(u => u.UserName == oUser.UserName);

            bool isValidPassword = BCrypt.Net.BCrypt.Verify(oUser.Password, user.Password);

            if (isValidPassword)
            {
                return user;
            }
            return null;
        }

        public User SignUp(User oUser)
        {
            oUser.Password = BCrypt.Net.BCrypt.HashPassword(oUser.Password);
            Global.Users.Add(oUser);

            User _newUser = CreateUser(oUser);

            return _newUser;
        }

        public IEnumerable<User> GetAllUsers()
        {
            UsersConnection _usersConnection = HttpContext.RequestServices.GetService(typeof(QuizManagerApi.Domain.Connections.UsersConnection)) as UsersConnection;
            //UsersConnection _usersConnection = UsersConnection;

            List<User> _users = _usersConnection.GetAllUsers();

            return _users.AsEnumerable();
        }

        public User GetUserById(int Id)
        {
            UsersConnection _usersConnection = HttpContext.RequestServices.GetService(typeof(QuizManagerApi.Domain.Connections.UsersConnection)) as UsersConnection;
            //UsersConnection _usersConnection = UsersConnection;

            User _user = _usersConnection.GetUserById(Id);

            return _user;
        }

        public User GetUserByUsername(string Username)
        {
            UsersConnection _usersConnection = HttpContext.RequestServices.GetService(typeof(QuizManagerApi.Domain.Connections.UsersConnection)) as UsersConnection;
            //UsersConnection _usersConnection = UsersConnection;

            User _user = _usersConnection.GetUserByUsername(Username);

            return _user;
        }

        //public bool ValidateCredentials(LogInCredentials SuppliedCredentials, LogInCredentials ActualCredentials)
        //{
        //    if (SuppliedCredentials.Username == ActualCredentials.Username)
        //        if (SuppliedCredentials.Password == ActualCredentials.Password)
        //            return true;
        //        else
        //            return false;
        //    else
        //        return false;
        //}

        public bool IsExistingUser(string Username)
        {
            UsersConnection _usersConnection = HttpContext.RequestServices.GetService(typeof(QuizManagerApi.Domain.Connections.UsersConnection)) as UsersConnection;
            //UsersConnection _usersConnection = UsersConnection;

            return _usersConnection.IsExistingUser(Username);
        }

        public User CreateUser(User NewUser)
        {
            UsersConnection _usersConnection = HttpContext.RequestServices.GetService(typeof(QuizManagerApi.Domain.Connections.UsersConnection)) as UsersConnection;
            //UsersConnection _usersConnection = UsersConnection;

            User _user = _usersConnection.CreateUser(NewUser);
            return _user;
        }

        public UserHasAccess MapNewUserToAccessLevel(int NewUserId, int AccessLevel)
        {
            if (AccessLevel == 0)
            {
                AccessLevel = 3;
            }
            UserAccessConnection _userAccessConnection = HttpContext.RequestServices.GetService(typeof(QuizManagerApi.Domain.Connections.UserAccessConnection)) as UserAccessConnection;
            //UserAccessConnection _userAccessConnection = UserAccessConnection;

            UserHasAccess _userAccessDetail = _userAccessConnection.MapNewUserToAccessLevel(NewUserId, AccessLevel);

            return _userAccessDetail;
        }
    }
}
