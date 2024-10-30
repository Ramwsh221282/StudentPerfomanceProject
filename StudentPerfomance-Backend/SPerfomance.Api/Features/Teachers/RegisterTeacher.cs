using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common;
using SPerfomance.Api.Features.TeacherDepartments.Contracts;
using SPerfomance.Api.Features.Teachers.Contracts;
using SPerfomance.Application.Departments.Commands.RegisterTeacher;
using SPerfomance.Application.Departments.DTO;
using SPerfomance.Application.Departments.Queries.GetDepartmentByName;
using SPerfomance.Domain.Models.TeacherDepartments;
using SPerfomance.Domain.Models.TeacherDepartments.Abstractions;
using SPerfomance.Domain.Models.Teachers;
using SPerfomance.Domain.Models.Teachers.Abstractions;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Api.Features.Teachers;

public static class RegisterTeacher
{
	public record Request(TeacherDepartmentContract Department, TeacherContract Teacher, TokenContract Token);

	public sealed class Endpoint : IEndpoint
	{
		public void MapEndpoint(IEndpointRouteBuilder app) =>
			app.MapPost($"{TeacherTags.Api}", Handler).WithTags(TeacherTags.Tag);
	}

	public async static Task<IResult> Handler(
		Request request,
		IUsersRepository users,
		ITeacherDepartmentsRepository departments,
		ITeachersRepository teachers
		)
	{
		if (!await new UserVerificationService(users).IsVerified(request.Token, UserRole.Administrator))
			return Results.BadRequest(UserTags.UnauthorizedError);

		Result<TeachersDepartments> department = await new GetDepartmentByNameQueryHandler(departments).Handle(request.Department);
		Result<Teacher> teacher = await new RegisterTeacherCommandHandler(teachers)
		.Handle(new(
			department.Value,
			request.Teacher.Name,
			request.Teacher.Surname,
			request.Teacher.Patronymic,
			request.Teacher.Job,
			request.Teacher.State
		));

		return teacher.IsFailure ?
			Results.BadRequest(teacher.Error.Description) :
			Results.Ok(teacher.Value.MapFromDomain());
	}
}
