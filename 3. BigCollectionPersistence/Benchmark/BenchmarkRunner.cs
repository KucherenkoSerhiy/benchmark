using Application;
using BenchmarkDotNet.Attributes;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Benchmark;

[MemoryDiagnoser]
public class EntryHandlerBenchmark
{
    private EntryHandler handler;
    private List<string> entries;

    [Params(1000, 10000, 20000)]
    public int NumEntries;

    private EntryContext entryContext;

    [GlobalSetup]
    public void Setup()
    {
        entryContext = new EntryContext();
        handler = new EntryHandler(new EntryRepository(entryContext));
        entries = Enumerable.Range(0, NumEntries).Select(i => $"Entry {i}").ToList();
    }

    [Benchmark]
    public Task ProcessEntriesTest() => handler.ProcessEntries(entries);
    
    [IterationCleanup]
    public void IterationCleanup()
    {
        entryContext.Database.ExecuteSqlRaw("TRUNCATE TABLE Entries");
        entryContext.SaveChanges();
    }
}