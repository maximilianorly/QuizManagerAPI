using System;
using System.Collections.Generic;
using QuizManagerApi.Domain.Models.LogInCredentials;
using QuizManagerApi.Domain.Models;

namespace QuizManagerApi.Domain.IService
{
    public interface IUserService
    {
        User SignUp(User oUser, int AccessLevelId);

        UserHasAccess Login(LogInCredentials oUser);

        IEnumerable<User> GetAllUsers();

        User GetUserById(int Id);

        User GetUserByUsername(string Username);

        bool IsExistingUser(string Username);

        User CreateUser(User NewUser);

        UserHasAccess MapNewUserToAccessLevel(int NewUserId, int AccessLevel);

    }
}
