# 1. Sync vs. Async

**Given**: 
- `AsyncApi`, an application to test
- `AsyncApi.ThirdPartyApi`, a third party API that simulates the 200 ms delay
- `AsyncApi.Benchmark`, a .NET project to perform benchmarks 

## How to Run
1. First, launch the `AsyncApi.ThirdPartyApi` project which will be called by the system under test.
2. Run the `AsyncApi.Benchmark` to get the metrics

## Benchmarks

| Method               | NumCalls |        Mean |    Error |   StdDev |     Gen0 |  Allocated |
|----------------------|----------|------------:|---------:|---------:|---------:|-----------:|
| SyncConcurrentCalls  | 1        |    211.8 ms |  0.80 ms |  0.75 ms |        - |   19.89 KB |
| AsyncConcurrentCalls | 1        |    211.4 ms |  0.81 ms |  0.67 ms |        - |   20.22 KB |
| SyncConcurrentCalls  | 10       |  2,121.3 ms |  5.75 ms |  5.38 ms |        - |   196.8 KB |
| AsyncConcurrentCalls | 10       |    211.4 ms |  0.89 ms |  0.83 ms |        - |  196.02 KB |
| SyncConcurrentCalls  | 50       | 10,616.7 ms |  9.27 ms |  7.74 ms |        - |  978.73 KB |
| AsyncConcurrentCalls | 50       |    218.1 ms |  2.30 ms |  2.15 ms |        - |  977.21 KB |
| SyncConcurrentCalls  | 100      | 21,228.5 ms | 11.27 ms | 10.54 ms |        - | 1956.97 KB |
| AsyncConcurrentCalls | 100      |    231.2 ms |  3.00 ms |  2.80 ms | 333.3333 |  1953.8 KB |

## Observations

1. **Performance**:
    - **Single Call (NumCalls = 1)**: Both synchronous and asynchronous methods have roughly the same mean time (~211
      ms), which is expected since they're essentially doing the same thing (i.e., a single call to the API with a 200ms
      delay).
    - **Concurrent Calls (NumCalls > 1)**: The asynchronous method performs remarkably better than the synchronous
      method. For example, with `NumCalls = 10`, the synchronous method takes approximately 10 times longer (2,121.3 ms)
      than the asynchronous method (211.4 ms). This behavior continues for higher concurrency levels (`NumCalls = 50`
      and `NumCalls = 100`). This demonstrates the power of asynchronous operations when dealing with IO-bound tasks (
      like API calls). While the synchronous method waits for each call to complete one by one, the asynchronous method
      fires off all the calls almost simultaneously.

2. **Memory**:
    - **Garbage Collection (Gen0)**: Only the asynchronous method with `NumCalls = 100` triggers a Gen0 garbage
      collection, which might be due to the larger number of tasks and allocations.
    - **Allocations**: Memory allocation increases linearly with the number of calls for both methods. However, there's
      not a significant difference between the synchronous and asynchronous methods in terms of allocated memory. This
      means the memory overhead of using `async/await` in this scenario is negligible.

### Key Takeaways:

1. **Asynchronous Advantage**: For concurrent calls, the asynchronous method significantly outperforms the synchronous
   method. This showcases the main advantage of asynchronous programming for IO-bound operations: it allows for
   non-blocking, concurrent execution.

2. **Scaling**: The asynchronous method scales much better as the number of concurrent calls increases. The synchronous
   method's mean execution time grows linearly with the number of calls, while the asynchronous method's time grows much
   slower.

3. **Memory Efficiency**: While there are some memory allocations, particularly with higher concurrency, the difference
   between synchronous and asynchronous methods isn't drastic.

### Recommendations:

1. **Adopt Asynchronous Calls**: Given the stark performance difference between the two methods for concurrent calls,
   it's clear that adopting asynchronous calls (when possible) for IO-bound operations will yield much better
   performance, especially under load.

2. **Further Testing**: It could be interesting to explore other benchmarks such as higher concurrency levels, different
   delays, error scenarios, and more to see how both methods respond under varying conditions.

3. **Monitor in Production**: Benchmarks provide a controlled environment, and while they offer valuable insights, it's
   also essential to monitor performance in real-world scenarios, such as in a production environment, to get a holistic
   view of system performance.

**In conclusion**, the results reaffirm the effectiveness of asynchronous programming for IO-bound tasks, especially when
concurrency is involved. This test demonstrates the importance of choosing the right programming model based on the
nature of the task and the expected system load.