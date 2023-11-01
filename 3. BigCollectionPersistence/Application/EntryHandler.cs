using System.Net.Http.Json;

namespace Application;

public class EntryHandler(IEntryRepository entryRepository)
{
    public async Task ProcessEntries(IEnumerable<string> entries)
    {
        const int batchSize = 1000; // Determine the right batch size based on system's capabilities and testing.
        foreach (var batch in entries.Batch(batchSize))
        {
            var entriesToSave = new List<Entry>(batchSize);
            var tasks = new List<Task>(batchSize);
            foreach (var entry in batch)
            {
                tasks.Add(ProcessEntryAsync(entry));
                entriesToSave.Add(new Entry(entry));
            }
            await Task.WhenAll(tasks);
            await entryRepository.AddEntriesAsync(entriesToSave);
            await entryRepository.SaveChangesAsync();
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
            // Handle or log exceptions as required.
        }
    }

    private static async Task CallThirdPartyAsync(string entry)
    {
        using HttpClient client = new();
        await client.PostAsJsonAsync("http://localhost:5105/dummy", entry);
    }
}