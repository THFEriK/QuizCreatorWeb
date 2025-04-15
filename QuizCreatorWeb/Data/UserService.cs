using QuizCreatorWeb.Data.Dto;
using System.Text.Json;
using System.Text;

namespace QuizCreatorWeb.Data
{
    public class UserService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<UserService> _logger;

        public UserService(HttpClient httpClient, ILogger<UserService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<UserDto?> CreateUser(UserDto user)
        {
            var json = JsonSerializer.Serialize(user);
            _logger.LogInformation("Sending JSON: {Json}", json);

            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("user", content);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("Failed to create user. Status code: {StatusCode}, Response: {Response}", response.StatusCode, await response.Content.ReadAsStringAsync());
                return null;
            }

            return await response.Content.ReadFromJsonAsync<UserDto>();
        }

        public async Task<UserDto?> GetUser(int userId)
        {
            var response = await _httpClient.GetAsync($"{userId}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<UserDto>();
            }
            else
            {
                _logger.LogWarning("Failed to fetch user with ID {UserId}. Status code: {StatusCode}", userId, response.StatusCode);
                return null;
            }
        }

        public async Task<string?> GetUserName(int userId)
        {
            var response = await _httpClient.GetAsync($"username/{userId}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                _logger.LogWarning("Failed to fetch username with ID {UserId}. Status code: {StatusCode}", userId, response.StatusCode);
                return null;
            }
        }

        public async Task<List<UserDto>?> GetAllUsers()
        {
            var response = await _httpClient.GetAsync("");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<UserDto>>();
            }
            else
            {
                _logger.LogWarning("Failed to fetch users. Status code: {StatusCode}", response.StatusCode);
                return null;
            }
        }

        public async Task<bool> UpdateUser(UserDto user)
        {
            var response = await _httpClient.PutAsJsonAsync($"{user.Id}", user);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("Failed to update user with ID {UserId}. Status code: {StatusCode}", user.Id, response.StatusCode);
            }

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteUser(int userId)
        {
            var response = await _httpClient.DeleteAsync($"{userId}");

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("Failed to delete user with ID {UserId}. Status code: {StatusCode}", userId, response.StatusCode);
            }

            return response.IsSuccessStatusCode;
        }
    }
}
