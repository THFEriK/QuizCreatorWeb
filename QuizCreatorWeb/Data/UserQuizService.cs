using QuizCreatorWeb.Data.Dto;
using System.Text.Json;
using System.Text;

namespace QuizCreatorWeb.Data
{
    public class UserQuizService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<UserQuizService> _logger;

        public UserQuizService(HttpClient httpClient, ILogger<UserQuizService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<UserQuizCompletionDto?> CreateQuizCompletion(UserQuizCompletionDto completionDto)
        {
            var json = JsonSerializer.Serialize(completionDto);
            _logger.LogInformation("Sending JSON: {Json}", json);

            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("quiz-completion", content);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("Failed to create quiz completion. Status code: {StatusCode}, Response: {Response}", response.StatusCode, await response.Content.ReadAsStringAsync());
                return null;
            }

            return await response.Content.ReadFromJsonAsync<UserQuizCompletionDto>();
        }

        public async Task<bool> SaveUserAnswer(UserQuizAnswerDto userAnswerDto)
        {
            var json = JsonSerializer.Serialize(userAnswerDto);
            _logger.LogInformation("Sending JSON: {Json}", json);

            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("quiz-answer", content);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("Failed to save user answer. Status code: {StatusCode}, Response: {Response}", response.StatusCode, await response.Content.ReadAsStringAsync());
                return false;
            }

            return true;
        }

        public async Task<List<UserQuizCompletionDto>> GetQuizCompletions(int quizId)
        {
            var response = await _httpClient.GetFromJsonAsync<List<UserQuizCompletionDto>>($"quiz-completion/quiz/{quizId}");
            return response ?? new List<UserQuizCompletionDto>();
        }

        public async Task<List<UserQuizAnswerDto>> GetUserAnswersByCompletionId(int completionId)
        {
            var response = await _httpClient.GetFromJsonAsync<List<UserQuizAnswerDto>>($"quiz-answer/completion/{completionId}");
            return response ?? new List<UserQuizAnswerDto>();
        }
    }
}
