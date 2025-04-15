namespace QuizCreatorWeb.Data.Dto
{
    public class UserQuizCompletionDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int QuizId { get; set; }
        public DateTime CompletionDate { get; set; }
    }
}
