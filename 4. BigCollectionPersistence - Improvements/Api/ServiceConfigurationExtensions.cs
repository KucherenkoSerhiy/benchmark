using Application;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Api;

public static class ServiceConfigurationExtensions
{
    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDbContext<EntryContext>(options =>
            options.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnection")));
        builder.Services.AddScoped<IEntryRepository, EntryRepository>();
        builder.Services.AddScoped<EntryHandler>();
    }
}