namespace QuizCreatorWeb.Data.Dto
{
    public class SingleChoiceOptionDto
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string OptionText { get; set; }
        public bool IsCorrect { get; set; }
    }
}
