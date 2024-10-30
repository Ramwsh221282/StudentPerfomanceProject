using Microsoft.AspNetCore.Mvc;

using SPerfomance.Api.Endpoints;
using SPerfomance.Application.EducationDirections.DTO;
using SPerfomance.Application.EducationDirections.Commands.UpdateEducationDirection;
using SPerfomance.Application.EducationDirections.Queries.GetEducationDirection;
using SPerfomance.Domain.Models.EducationDirections;
using SPerfomance.Domain.Models.EducationDirections.Abstractions;
using SPerfomance.Domain.Tools;
using SPerfomance.Api.Features.EducationDirections.Contracts;
using SPerfomance.Api.Features.Common;

namespace SPerfomance.Api.Features.EducationDirections;

public static class UpdateEducationDirection
{
	public record Request(EducationDirectionContract Initial, EducationDirectionContract Updated, TokenContract Token);

	public sealed class Endpoint : IEndpoint
	{
		public void MapEndpoint(IEndpointRouteBuilder app) =>
			app.MapPut($"{EducationDirectionTags.Api}", Handler).WithTags(EducationDirectionTags.Tag);
	}

	public static async Task<IResult> Handler([FromBody] Request request, IUsersRepository users, IEducationDirectionRepository repository)
	{
		if (!await new UserVerificationService(users).IsVerified(request.Token, UserRole.Administrator))
			return Results.BadRequest(UserTags.UnauthorizedError);

		Result<EducationDirection> direction = await new GetEducationDirectionQueryHandler(repository).Handle(request.Initial);

		direction = await new UpdateEducationDirectionCommandHandler(repository)
		.Handle(new(
			direction.Value,
			request.Updated.Name,
			request.Updated.Code,
			request.Updated.Type
		));

		return direction.IsFailure ?
			Results.BadRequest(direction.Error.Description) :
			Results.Ok(direction.Value.MapFromDomain());
	}
}
