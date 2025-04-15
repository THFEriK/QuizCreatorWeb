using QuizCreatorWeb.Data.Dto;

namespace QuizCreatorWeb.Models
{
    public class QuizAnswersModel
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public int QuestionTypeId { get; set; }
        public List<SingleChoiceOptionDto>? SingleChoiceOptions { get; set; }
        public List<MultipleChoiceOptionDto>? MultipleChoiceOptions { get; set; }
        public string? CorrectAnswer { get; set; }
        public string? UserAnswer { get; set; }
        public List<string>? UserAnswers { get; set; }
    }
}
