using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common;
using SPerfomance.Api.Features.TeacherDepartments.Contracts;
using SPerfomance.Application.Departments.Commands.CreateTeachersDepartment;
using SPerfomance.Application.Departments.DTO;
using SPerfomance.Domain.Models.TeacherDepartments;
using SPerfomance.Domain.Models.TeacherDepartments.Abstractions;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Api.Features.TeacherDepartments;

public static class RegisterTeacherDepartment
{
	public record Request(TeacherDepartmentContract Department, TokenContract Token);

	public sealed class Endpoint : IEndpoint
	{
		public void MapEndpoint(IEndpointRouteBuilder app) =>
			app.MapPost($"{TeacherDepartmentsTags.Api}", Handler).WithTags(TeacherDepartmentsTags.Tag);
	}

	public static async Task<IResult> Handler(Request request, IUsersRepository users, ITeacherDepartmentsRepository repository)
	{
		if (!await new UserVerificationService(users).IsVerified(request.Token, UserRole.Administrator))
			return Results.BadRequest(UserTags.UnauthorizedError);

		Result<TeachersDepartments> department = await new CreateTeachersDepartmentCommandHandler(repository).Handle(request.Department);
		return department.IsFailure ?
			Results.BadRequest(department.Error.Description) :
			Results.Ok(department.Value.MapFromDomain());
	}
}
