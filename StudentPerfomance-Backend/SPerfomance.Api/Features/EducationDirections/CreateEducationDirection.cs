using SPerfomance.Api.Endpoints;
using SPerfomance.Application.EducationDirections.DTO;
using SPerfomance.Application.EducationDirections.Commands.CreateEducationDirection;
using SPerfomance.Domain.Models.EducationDirections;
using SPerfomance.Domain.Models.EducationDirections.Abstractions;
using SPerfomance.Domain.Tools;
using SPerfomance.Api.Features.EducationDirections.Contracts;
using SPerfomance.Api.Features.Common;

namespace SPerfomance.Api.Features.EducationDirections;

public static class CreateEducationDirection
{
	public record Request(EducationDirectionContract Direction, TokenContract Token);

	public sealed class Endpoint : IEndpoint
	{
		public void MapEndpoint(IEndpointRouteBuilder app) =>
			app.MapPost($"{EducationDirectionTags.Api}", Handler).WithTags(EducationDirectionTags.Tag);
	}

	public static async Task<IResult> Handler(Request request, IUsersRepository users, IEducationDirectionRepository repository)
	{
		if (!await new UserVerificationService(users).IsVerified(request.Token, UserRole.Administrator))
			return Results.BadRequest(UserTags.UnauthorizedError);

		Result<EducationDirection> direction = await new CreateEducationDirectionCommandHandler(repository)
		.Handle(request.Direction);

		return direction.IsFailure ?
			Results.BadRequest(direction.Error.Description) :
			Results.Ok(direction.Value.MapFromDomain());
	}
}
