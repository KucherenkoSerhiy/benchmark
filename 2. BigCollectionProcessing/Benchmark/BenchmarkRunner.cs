using Application;
using BenchmarkDotNet.Attributes;

namespace Benchmark;

[MemoryDiagnoser]
public class EntryHandlerBenchmark
{
    private EntryHandler handler;
    private List<string> entries;

    [Params(1000, 10000, 20000)]
    public int NumEntries;

    [GlobalSetup]
    public void Setup()
    {
        handler = new EntryHandler();
        entries = Enumerable.Range(0, NumEntries).Select(i => $"Entry {i}").ToList();
    }

    [Benchmark]
    public Task ProcessEntriesTest() => handler.ProcessEntries(entries);
}