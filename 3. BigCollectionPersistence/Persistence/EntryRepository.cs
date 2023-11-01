using Application;

namespace Persistence;

public class EntryRepository : IEntryRepository
{
    private readonly EntryContext context;

    public EntryRepository(EntryContext context)
    {
        this.context = context;
    }

    public void AddEntryAsync(Entry entry)
    {
        context.Entries.Add(entry);
    }
    
    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
}