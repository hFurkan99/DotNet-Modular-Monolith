namespace Identity.Keycloak.Features.Login
{
    public record LoginRequest(string Username, string Password);

    public record LoginResponse(string? AccessToken, string? RefreshToken, int ExpiresIn);

    public class LoginEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/identity/login", async ([FromBody] LoginRequest request, ISender sender) =>
            {
                var command = new LoginCommand(request.Username, request.Password);
                var result = await sender.Send(command);

                var response = result.Adapt<LoginResponse>();

                return Results.Ok(response);
            })
            .Produces<LoginResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Login")
            .WithDescription("Login");
        }
    }
}
