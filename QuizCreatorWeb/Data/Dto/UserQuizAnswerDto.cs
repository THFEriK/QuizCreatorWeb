namespace QuizCreatorWeb.Data.Dto
{
    public class UserQuizAnswerDto
    {
        public int Id { get; set; }
        public int QuizCompletionId { get; set; }
        public int QuestionId { get; set; }
        public string Answer { get; set; }
        public DateTime SubmittedDate { get; set; }
    }
}

