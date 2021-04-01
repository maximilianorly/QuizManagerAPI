using System;
using Microsoft.AspNetCore.Http;
using QuizManagerApi.Domain.Models.LogInCredentials;
using QuizManagerApi.Domain.Models.User;
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

        public List<User> CreateUser(User NewUser)
        {
            UsersConnection _usersConnection = HttpContext.RequestServices.GetService(typeof(QuizManagerApi.Domain.Connections.UsersConnection)) as UsersConnection;

            
            var _users = _usersConnection.CreateUser(NewUser);
            return _users;
        }
    }
}
