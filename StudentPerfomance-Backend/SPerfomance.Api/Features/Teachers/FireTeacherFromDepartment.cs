using Microsoft.AspNetCore.Mvc;

using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common;
using SPerfomance.Api.Features.TeacherDepartments.Contracts;
using SPerfomance.Api.Features.Teachers.Contracts;
using SPerfomance.Application.Departments.Commands.FireTeacher;
using SPerfomance.Application.Departments.DTO;
using SPerfomance.Application.Departments.Queries.GetDepartmentByName;
using SPerfomance.Application.Departments.Queries.GetTeacherFromDepartment;
using SPerfomance.Domain.Models.TeacherDepartments;
using SPerfomance.Domain.Models.TeacherDepartments.Abstractions;
using SPerfomance.Domain.Models.Teachers;
using SPerfomance.Domain.Models.Teachers.Abstractions;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Api.Features.Teachers;

public static class FireTeacherFromDepartment
{
	public record Request(TeacherDepartmentContract Department, TeacherContract Teacher, TokenContract Token);

	public sealed class Endpoint : IEndpoint
	{
		public void MapEndpoint(IEndpointRouteBuilder app) =>
			app.MapDelete($"{TeacherTags.Api}", Handler).WithTags(TeacherTags.Tag);
	}

	public static async Task<IResult> Handler([FromBody] Request request, IUsersRepository users, ITeacherDepartmentsRepository departments, ITeachersRepository teachers)
	{
		if (!await new UserVerificationService(users).IsVerified(request.Token, UserRole.Administrator))
			return Results.BadRequest(UserTags.UnauthorizedError);

		Result<TeachersDepartments> department = await new GetDepartmentByNameQueryHandler(departments).Handle(request.Department);

		Result<Teacher> teacher = await new GetTeacherFromDepartmentQueryHandler()
		.Handle(new(
			department.Value,
			request.Teacher.Name,
			request.Teacher.Surname,
			request.Teacher.Patronymic,
			request.Teacher.Job,
			request.Teacher.State
			));

		teacher = await new FireTeacherCommandHandler(teachers).Handle(new(teacher.Value));
		return teacher.IsFailure ?
			Results.BadRequest(teacher.Error.Description) :
			Results.Ok(teacher.Value.MapFromDomain());
	}
}
