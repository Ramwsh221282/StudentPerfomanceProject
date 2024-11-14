using SPerfomance.Api.Endpoints;

namespace SPerfomance.Api.Features.Users;

public static class VerifyToken
{
    public record Request(string? Token);

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapPost($"{UserTags.Api}/verify", Handler).WithTags(UserTags.Tag);
    }

    public static async Task<IResult> Handler(Request request)
    {
        return await Task.Run(() =>
        {
            if (string.IsNullOrWhiteSpace(request.Token))
                return Results.Unauthorized();

            Token token = new Token(request.Token);
            if (token.IsExpired)
                return Results.Unauthorized();

            return Results.Ok();
        });
    }
}
