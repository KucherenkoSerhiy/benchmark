using System.Net.Http.Json;

namespace Application;

public class ThirdPartyClient(IHttpClientFactory httpClientFactory) : IThirdPartyClient
{
    public async Task CallThirdPartyAsync(string entry)
    {
        var httpClient = httpClientFactory.CreateClient("ThirdPartyApi");
        var response = await httpClient.PostAsJsonAsync("dummy", entry);
        response.EnsureSuccessStatusCode();
    }
}