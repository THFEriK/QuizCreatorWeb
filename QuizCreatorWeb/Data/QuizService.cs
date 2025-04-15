using QuizCreatorWeb.Data.Dto;
using System.Text.Json;
using System.Text;

namespace QuizCreatorWeb.Data
{
    public class QuizService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<QuizService> _logger;

        public QuizService(HttpClient httpClient, ILogger<QuizService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<QuizDto?> CreateQuiz(QuizDto quiz)
        {
            var json = JsonSerializer.Serialize(quiz);
            _logger.LogInformation("Sending JSON: {Json}", json);

            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("", content);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("Failed to create quiz. Status code: {StatusCode}, Response: {Response}", response.StatusCode, await response.Content.ReadAsStringAsync());
                return null;
            }

            return await response.Content.ReadFromJsonAsync<QuizDto>();
        }

        public async Task<List<QuizDto>?> GetAllQuizzes()
        {
            var response = await _httpClient.GetAsync("");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<QuizDto>>();
            }
            else
            {
                return null;
            }
        }

        public async Task<QuizDto?> GetQuiz(int quizId)
        {
            var response = await _httpClient.GetAsync($"{quizId}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<QuizDto>();
            }
            else
            {
                return null;
            }
        }

        public async Task<List<QuizDto>?> GetUserQuizzes()
        {
            var response = await _httpClient.GetAsync("user");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<QuizDto>>();
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> UpdateQuiz(QuizDto quiz)
        {
            var response = await _httpClient.PutAsJsonAsync($"{quiz.Id}", quiz);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteQuiz(int quizId)
        {
            var response = await _httpClient.DeleteAsync($"{quizId}");

            return response.IsSuccessStatusCode;
        }
    }
}
