using QuizCreatorWeb.Data.Dto;

namespace QuizCreatorWeb.Models
{
    public class QuizCompletionModel
    {
        public string UserName { get; set; }
        public UserQuizCompletionDto UserQuizCompletion { get; set; }
    }
}
