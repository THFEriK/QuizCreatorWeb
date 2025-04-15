namespace QuizCreatorWeb.Data.Dto
{
    public class MultipleChoiceOptionDto
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string OptionText { get; set; }
        public bool IsCorrect { get; set; }
    }
}
