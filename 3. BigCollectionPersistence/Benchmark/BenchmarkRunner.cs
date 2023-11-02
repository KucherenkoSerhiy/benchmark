using System.Text;
using System.Text.Json;
using BenchmarkDotNet.Attributes;

namespace Benchmark;

[MemoryDiagnoser]
public class EntryHandlerBenchmark
{
    private HttpClient client;
    private string requestBody;

    [Params(1000, 10000, 20000)] public int NumEntries;


    [GlobalSetup]
    public void Setup()
    {
        client = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5012")
        };

        var entries = Enumerable.Range(0, NumEntries).Select(i => $"Entry {i}").ToList();
        requestBody = JsonSerializer.Serialize(entries);
    }

    [Benchmark]
    public async Task ProcessEntriesTest()
    {
        var content = new StringContent(requestBody, Encoding.UTF8, "application/json");
        var response = await client.PostAsync("/entries", content);

        if (!response.IsSuccessStatusCode)
        {
            throw new InvalidOperationException("HTTP request for posting entries failed: " + response.StatusCode);
        }
    }

    [GlobalCleanup]
    public void Cleanup()
    {
        client?.Dispose();
    }
}