using System.Reflection.Metadata.Ecma335;
using MixAholic.Common;
using MixAholicCommon.Model;

namespace MixAholic.Service
{
    public class MixService : IMixService
    {
        public HttpClient HttpClient;
        public UserState UserState;
        public MixService(HttpClient httpClient, UserState userState)
        {
            HttpClient = httpClient;
            HttpClient.BaseAddress = new Uri("https://localhost:7065/");
            UserState = userState;
        }

		public async Task<Mix> CreateMix(Mix mix)
		{
            HttpClient.DefaultRequestHeaders.Add("Bearer", UserState.SessionKey);

			var response = await HttpClient.PostAsJsonAsync("api/Mix/CreateMix", mix);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<Mix>();
                return result;
            }
            else
            {
                var response2 = await response.Content.ReadAsStringAsync();
            }

            return null;
		}

		public async Task<Mix> GetMix(int mixId)
		{

			HttpClient.DefaultRequestHeaders.Add("Bearer", UserState.SessionKey);
			return await HttpClient.GetFromJsonAsync<Mix>($"api/Mix/GetMix/{mixId}");
		}

		public async Task<List<Mix>> GetMixes()
        {
            return await HttpClient.GetFromJsonAsync<List<Mix>>("api/Mix/GetMixes");
        }
        public async Task<bool> IsOwner(int mixId)
        {
			HttpClient.DefaultRequestHeaders.Add("Bearer", UserState.SessionKey);

			return await HttpClient.GetFromJsonAsync<bool>($"api/Mix/IsOwner/{mixId}");
		}


        public async Task UpdateMix(Mix mix)
        {
			HttpClient.DefaultRequestHeaders.Add("Bearer", UserState.SessionKey);

			var response = await HttpClient.PutAsJsonAsync("api/Mix/UpdateMix", mix);
		}
        public async Task RemoveMix(int mixId)
        {
			HttpClient.DefaultRequestHeaders.Add("Bearer", UserState.SessionKey);

			var response = await HttpClient.DeleteAsync($"api/Mix/RemoveMix/{mixId}");
		}

        public async Task<Rating> RateMix(Rating rating)
        {
			HttpClient.DefaultRequestHeaders.Add("Bearer", UserState.SessionKey);

			var response = await HttpClient.PostAsJsonAsync("api/Mix/RateMix", rating);
			if (response.IsSuccessStatusCode)
			{
				var result = await response.Content.ReadFromJsonAsync<Rating>();
				return result;
			}
            return null;

		}
	}
}
