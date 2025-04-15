namespace QuizCreatorWeb.Data.Dto
{
    public class QuestionDto
    {
        public int Id { get; set; }
        public int QuizId { get; set; }
        public string QuestionText { get; set; }
        public int QuestionTypeId { get; set; }
        public List<MultipleChoiceOptionDto>? MultipleChoiceOptions { get; set; } = null!;
        public List<SingleChoiceOptionDto>? SingleChoiceOptions { get; set; } = null!;
        public FreeTextDto? FreeText { get; set; } = null!;
    }
}
