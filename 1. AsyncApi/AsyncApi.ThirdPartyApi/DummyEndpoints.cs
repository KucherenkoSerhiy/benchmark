using Carter;

namespace AsyncApi.ThirdPartyApi;

public class DummyEndpoints: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/dummy", Delay200Milliseconds);
    }

    private static async Task<IResult> Delay200Milliseconds(int delayMilliseconds)
    {
        await Task.Delay(delayMilliseconds);
        return Results.Ok("Task delayed successfully! :)");
    }
}