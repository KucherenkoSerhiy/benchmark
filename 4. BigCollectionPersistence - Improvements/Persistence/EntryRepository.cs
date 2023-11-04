using Application;

namespace Persistence;

public class EntryRepository(EntryContext context) : IEntryRepository
{
    public async Task AddEntriesAsync(IEnumerable<Entry> entries)
    {
        await context.Entries.AddRangeAsync(entries);
    }
    
    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
}