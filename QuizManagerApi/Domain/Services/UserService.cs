using System;
using Microsoft.AspNetCore.Http;
using QuizManagerApi.Domain.Models.LogInCredentials;
using QuizManagerApi.Domain.Models;
using QuizManagerApi.Domain.Connections;
using System.Collections.Generic;
using QuizManagerApi.Domain.IService;
using QuizManagerApi.Common;
using System.Linq;
using MySql.Data.MySqlClient;

namespace QuizManagerApi.Domain.Services
{
    public class UserService : IUserService
    {
        //private UsersConnection UsersConnection { get; }
        //private UserAccessConnection UserAccessConnection { get; }

        //public UserService(UsersConnection usersConnection, UserAccessConnection userAccessConnection)
        //{
        //    UsersConnection = usersConnection;
        //    UserAccessConnection = userAccessConnection;
        //}

        private readonly UsersConnection _usersConnection;
        private readonly UserAccessConnection _userAccessConnection;

        public UserService(MySqlConnection conn)
        {
            _usersConnection = new UsersConnection(conn);
            _userAccessConnection = new UserAccessConnection(conn);
        }

        public UserHasAccess Login(LogInCredentials oUser)
        {
            var user = Global.Users.SingleOrDefault(u => u.UserName == oUser.Username);

            bool isValidPassword = BCrypt.Net.BCrypt.Verify(oUser.Password, user.Password);

            if (isValidPassword)
            {
                SetIsLoggedIn(user);
                User _user =  GetUserByUsername(oUser.Username);
                UserHasAccess _userHasAccess = GetUserAccessByUserId(_user.Id);
                return _userHasAccess;
            }
            return null;
        }

        public UserHasAccess GetUserAccessByUserId(int Id)
        {
            UserHasAccess _userHasAccess = _userAccessConnection.GetUserAccessByUserId(Id);

            return _userHasAccess;
        }

        public void SetIsLoggedIn(User oUser)
        {
            oUser.IsLoggedIn = !oUser.IsLoggedIn;

            _usersConnection.SetIsLoggedIn(oUser);
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
            //UsersConnection _usersConnection = UsersConnection;

            List<User> _users = _usersConnection.GetAllUsers();

            if (!Global.Users.Except(_users).Any())
            {
                foreach(User _user in _users)
                {
                    if (!Global.Users.Contains(_user))
                    {
                        Global.Users.Add(_user);
                    }
                }
            }

            return _users.AsEnumerable();
        }

        public User GetUserById(int Id)
        {
            //UsersConnection _usersConnection = UsersConnection;

            User _user = _usersConnection.GetUserById(Id);

            return _user;
        }

        public User GetUserByUsername(string Username)
        {
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
            //UsersConnection _usersConnection = UsersConnection;

            return _usersConnection.IsExistingUser(Username);
        }

        public User CreateUser(User NewUser)
        {
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
            //UserAccessConnection _userAccessConnection = UserAccessConnection;

            UserHasAccess _userAccessDetail = _userAccessConnection.MapNewUserToAccessLevel(NewUserId, AccessLevel);

            return _userAccessDetail;
        }

        public User Logout(int UserId)
        {
            User _user = GetUserById(UserId);

            SetIsLoggedIn(_user);

            return _user;
        }
    }
}
