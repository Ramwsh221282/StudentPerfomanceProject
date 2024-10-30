using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common;
using SPerfomance.Application.EducationDirections.DTO;
using SPerfomance.Domain.Models.EducationDirections;
using SPerfomance.Domain.Models.EducationDirections.Abstractions;

namespace SPerfomance.Api.Features.EducationDirections;

public static class GetPagedEducationDirections
{
	public record Request(PaginationContract Pagination, TokenContract Token);

	public sealed class Endpoint : IEndpoint
	{
		public void MapEndpoint(IEndpointRouteBuilder app) =>
			app.MapPost($"{EducationDirectionTags.Api}/byPage", Handler).WithTags(EducationDirectionTags.Tag);
	}

	public static async Task<IResult> Handler(Request request, IUsersRepository users, IEducationDirectionRepository repository)
	{
		if (!await new UserVerificationService(users).IsVerified(request.Token, UserRole.Administrator))
			return Results.BadRequest(UserTags.UnauthorizedError);

		IReadOnlyCollection<EducationDirection> directions = await repository.GetPaged(request.Pagination.Page, request.Pagination.PageSize);
		return Results.Ok(directions.Select(d => d.MapFromDomain()));
	}
}
