using Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<EntryContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnection")));
builder.Services.AddScoped<IEntryRepository, EntryRepository>();
builder.Services.AddScoped<EntryHandler>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/entries", async (
        [FromBody] IEnumerable<string> entries,
        EntryHandler entryHandler) =>
    {
        await entryHandler.ProcessEntries(entries);
    })
    .WithName("AddEntries")
    .WithOpenApi();

app.Run();