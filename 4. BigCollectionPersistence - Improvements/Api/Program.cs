using Api;
using Application;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureServices();

var app = builder.Build();

app.UseMiddleware();

// TODO: extract
app.MapPost("/entries", async (
        [FromBody] IEnumerable<string> entries,
        EntryHandler entryHandler) =>
    {
        await entryHandler.ProcessEntries(entries);
    })
    .WithName("AddEntries")
    .WithOpenApi();

app.Run();
