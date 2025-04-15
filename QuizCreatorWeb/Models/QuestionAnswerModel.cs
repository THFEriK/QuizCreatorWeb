using QuizCreatorWeb.Data.Dto;
using System.ComponentModel.DataAnnotations;

namespace QuizCreatorWeb.Models
{
    public class QuestionAnswerModel
    {
        [Required]
        public int QuestionId { get; set; }
        [Required]
        public string QuestionText { get; set; }
        public int QuestionTypeId { get; set; }
        public List<SingleChoiceOptionDto>? SingleChoiceOptions { get; set; }
        public List<MultipleChoiceOptionDto>? MultipleChoiceOptions { get; set; }
        public string? SelectedSingleChoice { get; set; }
        public List<MultipleChoiceOptionSelection> MultipleChoiceSelections { get; set; } = new();
        public string? FreeTextAnswer { get; set; }
    }

    public class MultipleChoiceOptionSelection
    {
        public string OptionText { get; set; } = string.Empty;
        public bool IsSelected { get; set; }
    }
}
