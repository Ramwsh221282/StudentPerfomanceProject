using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common;
using SPerfomance.Domain.Models.TeacherDepartments.Abstractions;

namespace SPerfomance.Api.Features.TeacherDepartments;

public static class CountTeacherDepartments
{
	public record Request(TokenContract Contract);

	public sealed class Endpoint : IEndpoint
	{
		public void MapEndpoint(IEndpointRouteBuilder app) =>
			app.MapPost($"{TeacherDepartmentsTags.Api}/count", Handler).WithTags(TeacherDepartmentsTags.Tag);
	}

	public static async Task<IResult> Handler(Request request, IUsersRepository users, ITeacherDepartmentsRepository repository)
	{
		if (!await new UserVerificationService(users).IsVerified(request.Contract, UserRole.Administrator))
			return Results.BadRequest(UserTags.UnauthorizedError);

		int count = await repository.Count();
		return Results.Ok(count);
	}
}
