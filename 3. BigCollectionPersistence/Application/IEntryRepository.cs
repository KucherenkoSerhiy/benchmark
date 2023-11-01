namespace Application;

public interface IEntryRepository
{
    void AddEntryAsync(Entry entry);
    Task SaveChangesAsync();
}