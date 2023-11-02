namespace Api;

public static class WebApplicationConfigurationExtensions
{
    public static void UseMiddleware(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseHttpsRedirection();
    }
}