using QuizCreatorWeb.Data.Dto;
using System.Text.Json;
using System.Text;

namespace QuizCreatorWeb.Data
{
    public class QuestionService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<QuestionService> _logger;

        public QuestionService(HttpClient httpClient, ILogger<QuestionService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<QuestionDto?> CreateQuestion(QuestionDto question)
        {
            var json = JsonSerializer.Serialize(question);
            _logger.LogInformation("Sending JSON: {Json}", json);

            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("", content);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("Failed to create question. Status code: {StatusCode}, Response: {Response}", response.StatusCode, await response.Content.ReadAsStringAsync());
                return null;
            }

            return await response.Content.ReadFromJsonAsync<QuestionDto>();
        }

        public async Task<QuestionDto?> GetQuestion(int questionId)
        {
            var response = await _httpClient.GetAsync($"{questionId}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<QuestionDto>();
            }
            else
            {
                _logger.LogWarning("Failed to retrieve question with ID {QuestionId}. Status code: {StatusCode}", questionId, response.StatusCode);
                return null;
            }
        }

        public async Task<List<QuestionDto>?> GetQuestionsByQuizId(int quizId)
        {
            var response = await _httpClient.GetAsync($"quiz/{quizId}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<QuestionDto>>();
            }
            else
            {
                _logger.LogWarning("Failed to retrieve questions for quiz ID {QuizId}. Status code: {StatusCode}", quizId, response.StatusCode);
                return null;
            }
        }

        public async Task<bool> UpdateQuestion(QuestionDto question)
        {
            var response = await _httpClient.PutAsJsonAsync($"{question.Id}", question);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("Failed to update question with ID {QuestionId}. Status code: {StatusCode}", question.Id, response.StatusCode);
            }

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteQuestion(int questionId)
        {
            var response = await _httpClient.DeleteAsync($"{questionId}");

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("Failed to delete question with ID {QuestionId}. Status code: {StatusCode}", questionId, response.StatusCode);
            }

            return response.IsSuccessStatusCode;
        }
    }
}
