namespace QuizManagerApi.Domain.Models.UserHasAccess
{
    public class UserHasAccess
    {
        public int Id { get; set; }
        public int AccessLevelId { get; set; }
        public int UserId { get; set; }
    }
}
