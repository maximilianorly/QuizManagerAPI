﻿namespace QuizManagerApi.Domain.Models.User
{
    public class Ques
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public bool IsActive { get; set; }
        public string Created { get; set; }
        public string Modified { get; set; }
    }
}