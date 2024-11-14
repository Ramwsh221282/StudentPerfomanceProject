using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common;

namespace SPerfomance.Api.Features.Users;

public static class GetUsersCount
{
    public record Request(TokenContract Token);

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapPost($"{UserTags.Api}/count", Handler).WithTags($"{UserTags.Tag}");
    }

    public static async Task<IResult> Handler(Request request, IUsersRepository repository)
    {
        if (
            !await new UserVerificationService(repository).IsVerified(
                request.Token,
                UserRole.Administrator
            )
        )
            return Results.BadRequest(
                "Ваша сессия не удовлетворяет требованиям. Необходимо авторизоваться."
            );

        int count = await repository.Count();
        return Results.Ok(count);
    }
}
