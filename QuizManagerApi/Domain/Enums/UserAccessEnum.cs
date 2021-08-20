using System;
namespace QuizManagerApi.Domain.Enums
{
    public enum UserAccessEnum
    {
        Unknown = 0,
        Admin = 1,
        UserWithAccess = 2,
        Restricted = 3,
    }
}
