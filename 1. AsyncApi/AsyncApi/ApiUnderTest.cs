namespace _AsyncApi;

public class ApiUnderTest
{
    public static async Task<string> CallAsync()
    {
        using HttpClient client = new();
        var response = await client.GetStringAsync("http://localhost:5223/dummy?delayMilliseconds=200");
        return response;
    }
}