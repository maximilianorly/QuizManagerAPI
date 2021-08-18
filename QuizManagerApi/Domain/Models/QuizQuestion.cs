namespace QuizManagerApi.Domain.Models.QuizQuestion
{
    public class QuizQuestion
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public bool IsActive { get; set; }
        public string Created { get; set; }
        public string Modified { get; set; }
        public int QuizId { get; set; }
    }
}
