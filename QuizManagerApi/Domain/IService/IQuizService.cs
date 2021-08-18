using System;
using System.Collections.Generic;
using QuizManagerApi.Domain.Models;

namespace QuizManagerApi.Domain.IService
{
    public interface IQuizService
    {
        IEnumerable<Quiz> GetAllActiveQuizzes();
    }
}
