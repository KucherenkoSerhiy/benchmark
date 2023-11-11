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
        builder.Services.AddSingleton<IThirdPartyClient, ThirdPartyClient>();
        builder.Services.AddHttpClient("ThirdPartyApi", client =>
        {
            client.BaseAddress = new Uri(builder.Configuration["BaseUrl"] ??
                                         throw new InvalidOperationException("BaseUrl not specified"));
        });
    }
}