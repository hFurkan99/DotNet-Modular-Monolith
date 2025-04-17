namespace Identity.Keycloak.Features.Login
{
    public record LoginCommand(string Username, string Password)
        : ICommand<LoginResult>;

    public record LoginResult(string? AccessToken, string? RefreshToken, int ExpiresIn);

    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("Username is required");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
        }
    }

    internal class LoginHandler(HttpClient httpClient, IConfiguration configuration) : ICommandHandler<LoginCommand, LoginResult>
    {
        public async Task<LoginResult> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var tokenUrl = configuration["Keycloak:token-url"];
            var resource = configuration["Keycloak:resource"];
            var clientSecret = configuration["Keycloak:client-secret"];

            var form = new Dictionary<string, string>
            {
                {"grant_type", "password"},
                {"client_id", resource!},
                {"client_secret", clientSecret!},
                {"username", request.Username},
                {"password", request.Password}
            };

            var response = await httpClient.PostAsync(tokenUrl, new FormUrlEncodedContent(form), cancellationToken);
            var content = await response.Content.ReadAsStringAsync(cancellationToken);

            if (!response.IsSuccessStatusCode)
                throw new Exception("Login failed: " + content);

            var json = JsonSerializer.Deserialize<JsonElement>(content);

            return new LoginResult(
                AccessToken: json.GetProperty("access_token").GetString(),
                RefreshToken: json.GetProperty("refresh_token").GetString(),
                ExpiresIn: json.GetProperty("expires_in").GetInt32()
            );
        }
    }
}
