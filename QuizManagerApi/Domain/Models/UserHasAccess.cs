namespace QuizManagerApi.Domain.Models
{
    public class UserHasAccess
    {
        public int Id { get; set; }
        public int AccessLevelId { get; set; }
        public int UserId { get; set; }
    }
}
