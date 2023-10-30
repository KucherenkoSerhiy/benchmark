using System.Net.Http.Json;

namespace Application;

public class EntryHandler
{
    public async Task ProcessEntries(IEnumerable<string> entries)
    {
        const int batchSize = 1000; // Determine the right batch size based on system's capabilities and testing.
        foreach (var batch in entries.Batch(batchSize))
        {
            var tasks = batch.Select(ProcessEntryAsync);
            await Task.WhenAll(tasks);
        }
    }

    private async Task ProcessEntryAsync(string entry)
    {
        try
        {
            await CallThirdPartyAsync(entry);
        }
        catch (Exception ex)
        {
            // Log the error or handle it based on requirements.
            // The operation continues even if one fails.
        }
    }

    private static async Task CallThirdPartyAsync(string entry)
    {
        using HttpClient client = new();
        await client.PostAsJsonAsync("http://localhost:5105/dummy", entry);
    }
}