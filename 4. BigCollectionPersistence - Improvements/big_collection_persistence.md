# 4. Improvements

**Given**:
- A REST API that processes a batch of entries using Entity Framework Core for database persistence.
- A third-party service that the API interacts with to process each entry.
- A benchmark to measure the performance characteristics of the API and its interaction with the third-party service.

## Requirements

- .NET SDK installed on the system.
- Entity Framework Core CLI tools installed.

## Installation
[big_collection_persistence.md](..%2F3.%20BigCollectionPersistence%2Fbig_collection_persistence.md)
Before running the benchmarks, install the necessary tool:

```shell
dotnet tool install --global dotnet-ef
```

## Setup

Assuming you are in the root path of the solution, follow these steps:

1. **Add a Migration**:
   ```shell
   dotnet ef migrations add MigrationName -p .\Persistence\ -s .\Api\
   ```

2. **Update the Database**:
   ```shell
   dotnet ef database update -p .\Persistence\ -s .\Api\
   ```

## How to Run

1. Start the **ThirdParty** service to handle the incoming requests from the API.
2. Launch the **API** project to accept and process HTTP requests.
3. Ensure the target database is up and running with the correct schema applied.
4. Execute the benchmark to simulate HTTP POST requests to the API and measure performance metrics.

## Benchmarks

Before:

| Method             | NumEntries |        Mean |    Error |   StdDev | Median     | Allocated  |
|--------------------|------------|------------:|---------:|---------:|------------:|-----------:|
| PostEntries        | 1000       |    930.1 ms |  72.06 ms| 212.5 ms |  1,047.0 ms |  15.56 KB  |
| PostEntries        | 10000      |  7,887.6 ms | 434.49 ms|1,281.1 ms|  7,971.4 ms | 129.73 KB  |
| PostEntries        | 20000      | 17,843.3 ms | 537.05 ms|1,566.6 ms| 18,041.1 ms | 266.55 KB  |

After using HttpClientFactory:

| Method             | NumEntries | Mean     | Error    | StdDev   | Allocated |
|--------------------|------------|----------|----------|----------|-----------|
| ProcessEntriesTest | 1000       | 1.373 s  | 0.0394 s | 0.1110 s | 15.47 KB  |
| ProcessEntriesTest | 10000      | 13.531 s | 0.2678 s | 0.5590 s | 129.83 KB |
| ProcessEntriesTest | 20000      | 26.985 s | 0.5385 s | 0.8541 s | 267.46 KB |

There seem to be an increase in the Mean execution time compared to the previous results.

## Observations

1. **Performance**:
   - The mean processing time rises sharply with the number of entries, pointing to potential scalability bottlenecks.
   - A high standard deviation indicates significant variability in response times, which may be affected by the third-party service's performance.

2. **Memory**:
   - The relatively low memory allocation may not accurately reflect memory efficiency, given the simplicity of the data processed (single-string entries) and the absence of HTTP client reuse.

### Key Takeaways

1. **Scalability**: The current architecture requires refinement to handle larger volumes efficiently, particularly regarding database and third-party service interactions.

2. **Questionable Memory Efficiency**: Given the simplicity of the data and the potential for HTTP client inefficiencies, the memory allocation figures should be scrutinized further.

### Further Improvements

1. **Database Optimization**: Examine EF Core's database interactions for potential enhancements, such as improved query performance or more efficient update mechanisms.

2. **HTTP Client Management**: Implement a more robust HTTP client management strategy to reduce overhead and potentially decrease memory usage.

3. **Concurrency and Parallelism**: Explore increasing concurrency in the API's design, particularly how it handles simultaneous requests, to improve throughput.

4. **Monitoring and Profiling**: Employ thorough monitoring and profiling to identify precise performance bottlenecks and to guide targeted optimizations.

**In Conclusion**, the integration of EF Core with an HTTP-triggered approach to processing entries demonstrates the importance of efficient database interaction, client management, and concurrency control. The performance can be significantly affected by these factors, especially when scaling up the number of entries. Regular performance evaluations and optimizations are essential to ensure the system remains effective and efficient as the load increases.