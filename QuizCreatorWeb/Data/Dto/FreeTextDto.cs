namespace QuizCreatorWeb.Data.Dto
{
    public class FreeTextDto
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string? CorrectAnswer { get; set; }
    }
}
