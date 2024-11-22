using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common;
using SPerfomance.Api.Features.Users.Contracts;
using SPerfomance.Application.Users.DTO;

namespace SPerfomance.Api.Features.Users;

public static class GetUsersByFilter
{
    public record Request(UserContract User, PaginationContract Pagination, TokenContract Token);

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapPost($"{UserTags.Api}/filter", Handler).WithTags(UserTags.Tag);
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
            return Results.BadRequest(UserTags.UnauthorizedError);

        var users = await repository.GetFiltered(
            request.User.Name,
            request.User.Surname,
            request.User.Patronymic,
            request.User.Email,
            request.User.Role,
            request.Pagination.Page,
            request.Pagination.PageSize,
            ct
        );
        return Results.Ok(users.Select(u => u.MapFromDomain()));
    }
}
