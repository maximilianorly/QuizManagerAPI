using System;
using QuizManagerApi.Domain.Models.LogInCredentials;
using QuizManagerApi.Domain.Models.User;

namespace QuizManagerApi.Domain.Services
{
    public class UserService
    {
        public UserService()
        {
            
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
    }
}
