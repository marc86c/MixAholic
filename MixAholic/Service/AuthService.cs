using System.Text.Json;
using MixAholicCommon.Model;

namespace MixAholic.Service
{
    public class AuthService : IAuthService
    {
        public HttpClient HttpClient;
        public AuthService(HttpClient httpClient)
        {
            HttpClient = httpClient;
            HttpClient.BaseAddress = new Uri("https://localhost:7065/");
        }

        public async Task<string> Login(string user, string password)
        {
            var loginModel = new AuthModel { Username = user, Password = password };
            var response = await HttpClient.PostAsJsonAsync("api/Auth/Login", loginModel);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            // Handle error response
            return null;
        }

        public async Task<bool> Register(string user, string password)
        {
            var registerModel = new AuthModel { Username = user, Password = password };
            var response = await HttpClient.PostAsJsonAsync("api/Auth/Register", registerModel);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<bool>();
                return result;
            }

            // Handle error response
            return false;
        }

    }
}
