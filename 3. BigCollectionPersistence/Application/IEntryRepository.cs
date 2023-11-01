namespace Application;

public interface IEntryRepository
{
    Task AddEntriesAsync(IEnumerable<Entry> entries);
    Task SaveChangesAsync();
}