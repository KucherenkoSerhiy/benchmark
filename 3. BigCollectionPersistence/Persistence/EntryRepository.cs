using Application;

namespace Persistence;

public class EntryRepository : IEntryRepository
{
    private readonly EntryContext context;

    public EntryRepository(EntryContext context)
    {
        this.context = context;
    }

    public async Task AddEntriesAsync(IEnumerable<Entry> entries)
    {
        await context.Entries.AddRangeAsync(entries);
    }
    
    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
}