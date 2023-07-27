using Noa.ContactNotifier.Contract.v1;
using Noa.ContactNotifier.Docker.Configuration;
using System.Text.Json;

namespace Noa.ContactNotifier.Docker.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _httpClient;

        public AuthenticationService(ServiceConfiguration config)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(config.Api.AuthenticationServiceURL);
        }

        public async Task<UserData> GetPublicUserData(Guid userId)
        {
            var uri = new Uri(_httpClient.BaseAddress, $"people/username/{userId}");
            var response = await _httpClient.GetAsync(uri);
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<UserData>(json);
        }
    }
}
