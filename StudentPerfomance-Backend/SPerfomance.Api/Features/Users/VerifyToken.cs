using System.Text;
using SPerfomance.Api.Endpoints;

namespace SPerfomance.Api.Features.Users;

public static class VerifyToken
{
    public record Request(string? Token);

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapPost($"{UserTags.Api}/verify", Handler)
                .WithTags(UserTags.Tag)
                .WithOpenApi()
                .WithName("VerifyToken")
                .WithDescription(new StringBuilder().AppendLine("Проверка токена JWT").ToString());
    }

    public static async Task<IResult> Handler(Request request, CancellationToken ct)
    {
        return await Task.Run(
            () =>
            {
                if (string.IsNullOrWhiteSpace(request.Token))
                    return Results.Unauthorized();

                var token = new Token(request.Token);
                return token.IsExpired ? Results.Unauthorized() : Results.Ok();
            },
            ct
        );
    }
}
