using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using QuizCreatorWeb.Data.Dto;
using System.Xml.Linq;
using System.IdentityModel.Tokens.Jwt;

namespace QuizCreatorWeb.Data
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<AuthenticationService> _logger;

        public event Action? AuthenticationStateChanged;

        public AuthService(HttpClient httpClient, ILogger<AuthenticationService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<SignInResponseDto> SignInAsync(SignInDto signInData)
        {
            _logger.LogInformation("SignIn attempt for email: {Email}", signInData.Email);

            var content = new StringContent(JsonSerializer.Serialize(signInData), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/auth/signin", content);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("SignIn failed for email: {Email}", signInData.Email);
                return null;
            }

            return await response.Content.ReadFromJsonAsync<SignInResponseDto>();
        }

        public async Task<bool> SignUp(string email, string password, string name)
        {
            _logger.LogInformation("SignUp attempt for email: {Email}", email);

            var signUpData = new SignUpDto() {
                UserName = name,
                Email = email,
                Password = password
            };
            var content = new StringContent(JsonSerializer.Serialize(signUpData), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/auth/signup", content);

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            return true;
        }
    }

    public class SignInResponseDto
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }

        [JsonPropertyName("user")]
        public UserDto User { get; set; }
    }

    public class SignUpResponseDto
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }

        [JsonPropertyName("user")]
        public UserDto User { get; set; }
    }
}
