using System.ComponentModel.DataAnnotations;

namespace QuizCreatorWeb.Models
{
    public class QuizFillModel
    {
        [Required]
        public int QuizId { get; set; }

        [Required]
        public List<QuestionAnswerModel> Questions { get; set; } = new();
    }
}
