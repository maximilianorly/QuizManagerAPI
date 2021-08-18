using System;
namespace QuizManagerApi.Domain.Models
{
    public class Quiz
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; }
    }
}
