using Carter;

namespace ThirdParty;

public class DummyEndpoints: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/dummy", Delay200Milliseconds);
    }

    private async Task<IResult> Delay200Milliseconds()
    {
        await Task.Delay(200);
        return Results.Ok("Task delayed successfully! :)");
    }
}