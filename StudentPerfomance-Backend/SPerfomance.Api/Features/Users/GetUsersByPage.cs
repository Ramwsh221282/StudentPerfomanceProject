using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common;
using SPerfomance.Application.Users.DTO;

namespace SPerfomance.Api.Features.Users;

public static class GetUsersByPage
{
    public record Request(PaginationContract Pagination, TokenContract Token);

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapPost($"{UserTags.Api}/byPage", Handler).WithTags(UserTags.Tag);
    }

    public static async Task<IResult> Handler(
        Request request,
        IUsersRepository repository,
        CancellationToken ct
    )
    {
        if (
            !await new UserVerificationService(repository).IsVerified(
                request.Token,
                UserRole.Administrator,
                ct
            )
        )
            return Results.BadRequest(
                "Ваша сессия не удовлетворяет требованиям. Необходимо авторизоваться."
            );

        var users = await repository.GetPaged(
            request.Pagination.Page,
            request.Pagination.PageSize,
            ct
        );
        return Results.Ok(users.Select(u => u.MapFromDomain()));
    }
}
