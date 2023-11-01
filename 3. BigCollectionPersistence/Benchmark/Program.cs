using BenchmarkDotNet.Running;

namespace Benchmark;

static class Program
{
    static void Main(string[] args)
    {
        var summary = BenchmarkRunner.Run<EntryHandlerBenchmark>();
    }
}