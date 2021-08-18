using System;
using System.Collections.Generic;
using QuizManagerApi.Domain.Models.User;

namespace QuizManagerApi.Common
{
    public static class Global
    {
            public static List<User> Users { get; set; } = new List<User>();
    }
}
