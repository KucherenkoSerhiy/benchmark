using _AsyncApi;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace AsyncApi.Benchmark;

[MemoryDiagnoser]
public class ApiBenchmark
{
    [Params(1, 10, 50, 100)] // Vary the level of concurrency
    public int NumCalls;
        
    public static void Main(string[] args)
    {
        var summary = BenchmarkRunner.Run<ApiBenchmark>();
    }

    [Benchmark]
    public void SyncConcurrentCalls() => TestSyncConcurrentCalls(NumCalls);

    [Benchmark]
    public Task AsyncConcurrentCalls() => TestAsyncConcurrentCalls(NumCalls);

    private async Task TestAsyncConcurrentCalls(int numCalls)
    {
        var tasks = new List<Task<string>>();
    
        for (int i = 0; i < numCalls; i++)
        {
            tasks.Add( ApiUnderTest.CallAsync());
        }

        await Task.WhenAll(tasks);
    }

    private void TestSyncConcurrentCalls(int numCalls)
    {
        for (var i = 0; i < numCalls; i++)
        {
            _ = ApiUnderTest.CallAsync().Result;
        }
    }
}