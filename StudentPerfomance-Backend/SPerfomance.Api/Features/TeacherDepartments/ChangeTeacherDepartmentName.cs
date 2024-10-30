using Microsoft.AspNetCore.Mvc;

using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common;
using SPerfomance.Api.Features.TeacherDepartments.Contracts;
using SPerfomance.Application.Departments.Commands.ChangeTeachersDepartmentName;
using SPerfomance.Application.Departments.DTO;
using SPerfomance.Application.Departments.Queries.GetDepartmentByName;
using SPerfomance.Domain.Models.TeacherDepartments;
using SPerfomance.Domain.Models.TeacherDepartments.Abstractions;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Api.Features.TeacherDepartments;

public static class ChangeTeacherDepartmentName
{
	public record Request(TeacherDepartmentContract Initial, TeacherDepartmentContract Updated, TokenContract Token);

	public sealed class Endpoint : IEndpoint
	{
		public void MapEndpoint(IEndpointRouteBuilder app) =>
			app.MapPut($"{TeacherDepartmentsTags.Api}", Handler).WithTags(TeacherDepartmentsTags.Tag);
	}

	public static async Task<IResult> Handler(
		[FromBody] Request request,
		IUsersRepository users,
		ITeacherDepartmentsRepository repository
		)
	{
		if (!await new UserVerificationService(users).IsVerified(request.Token, UserRole.Administrator))
			return Results.BadRequest(UserTags.UnauthorizedError);

		Result<TeachersDepartments> department = await new GetDepartmentByNameQueryHandler(repository).Handle(request.Initial);
		department = await new ChangeTeachersDepartmentNameCommandHandler(repository)
		.Handle(new(department.Value, request.Updated.Name));

		return department.IsFailure ?
			Results.BadRequest(department.Error.Description) :
			Results.Ok(department.Value.MapFromDomain());
	}
}
