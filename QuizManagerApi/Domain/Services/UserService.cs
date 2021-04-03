using System;
using Microsoft.AspNetCore.Http;
using QuizManagerApi.Domain.Models.LogInCredentials;
using QuizManagerApi.Domain.Models.User;
using QuizManagerApi.Domain.Models.UserHasAccess;
using QuizManagerApi.Domain.Connections;
using System.Collections.Generic;

namespace QuizManagerApi.Domain.Services
{
    public class UserService
    {
        public HttpContext HttpContext { get; }

        public UserService(HttpContext httpContext)
        {
            HttpContext = httpContext;
        }

        public IEnumerable<User> GetAllUsers()
        {
            UsersConnection _usersConnection = HttpContext.RequestServices.GetService(typeof(QuizManagerApi.Domain.Connections.UsersConnection)) as UsersConnection;

            var _users = _usersConnection.GetAllUsers();

            return _users;
        }

        public User GetUserById(int Id)
        {
            UsersConnection _usersConnection = HttpContext.RequestServices.GetService(typeof(QuizManagerApi.Domain.Connections.UsersConnection)) as UsersConnection;

            var _user = _usersConnection.GetUserById(Id);

            return _user;
        }

        public User GetUserByUsername(string Username)
        {
            UsersConnection _usersConnection = HttpContext.RequestServices.GetService(typeof(QuizManagerApi.Domain.Connections.UsersConnection)) as UsersConnection;

            var _user = _usersConnection.GetUserByUsername(Username);

            return _user;
        }

        public bool ValidateCredentials(LogInCredentials SuppliedCredentials, LogInCredentials ActualCredentials)
        {
            if (SuppliedCredentials.Username == ActualCredentials.Username)
                if (SuppliedCredentials.Password == ActualCredentials.Password)
                    return true;
                else
                    return false;
            else
                return false;
        }

        public bool IsExistingUser(string Username)
        {
            UsersConnection _usersConnection = HttpContext.RequestServices.GetService(typeof(QuizManagerApi.Domain.Connections.UsersConnection)) as UsersConnection;

            return _usersConnection.IsExistingUser(Username);
        }

        public User CreateUser(User NewUser)
        {
            UsersConnection _usersConnection = HttpContext.RequestServices.GetService(typeof(QuizManagerApi.Domain.Connections.UsersConnection)) as UsersConnection;

            var _user = _usersConnection.CreateUser(NewUser);
            return _user;
        }

        public UserHasAccess MapNewUserToAccessLevel(int NewUserId, int AccessLevel)
        {
            if (AccessLevel == 0)
            {
                AccessLevel = 3;
            }
            UserAccessConnection _userAccessConnection = HttpContext.RequestServices.GetService(typeof(QuizManagerApi.Domain.Connections.UserAccessConnection)) as UserAccessConnection;

            UserHasAccess _userAccessDetail = _userAccessConnection.MapNewUserToAccessLevel(NewUserId, AccessLevel);

            return _userAccessDetail;
        }
    }
}
