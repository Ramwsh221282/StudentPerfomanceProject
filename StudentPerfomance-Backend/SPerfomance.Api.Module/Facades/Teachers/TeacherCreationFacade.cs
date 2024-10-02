using Microsoft.AspNetCore.Mvc;

using SPerfomance.Api.Module.Converters.TeacherDepartments;
using SPerfomance.Api.Module.Converters.Teachers;
using SPerfomance.Api.Module.Responses.Teachers;
using SPerfomance.Application.Shared.Module.Schemas.Teachers;
using SPerfomance.Application.Teachers.Module.Commands.Create;
using SPerfomance.DataAccess.Module.Shared;
using SPerfomance.DataAccess.Module.Shared.Repositories.Teachers;
using SPerfomance.DataAccess.Module.Shared.Repositories.Teachers.Expressions;
using SPerfomance.DataAccess.Module.Shared.Repositories.TeachersDepartments;
using SPerfomance.DataAccess.Module.Shared.Repositories.TeachersDepartments.Expressions;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments;
using SPerfomance.Domain.Module.Shared.Entities.Teachers;

namespace SPerfomance.Api.Module.Facades.Teachers;

internal sealed class TeacherCreationFacade(TeacherSchema teacher) : IFacade<TeacherResponse>
{
	private readonly TeacherSchema _teacher = teacher;
	public async Task<ActionResult<TeacherResponse>> Process()
	{
		TeacherRepositoryObject teacherParameter = _teacher.ToRepositoryObject();
		DepartmentRepositoryObject departmentParameter = _teacher.Department.ToRepositoryObject();
		IRepositoryExpression<Teacher> checkDublicate = TeachersRepositoryExpressionFactory.CreateHasTeacher(teacherParameter);
		IRepositoryExpression<TeachersDepartment> findDepartment = TeachersDepartmentsExpressionsFactory.HasDepartment(departmentParameter);
		IRepository<Teacher> teachers = RepositoryProvider.CreateTeachersRepository();
		IRepository<TeachersDepartment> departments = RepositoryProvider.CreateDepartmentsRepository();
		TeacherCreateCommand command = new TeacherCreateCommand(_teacher, checkDublicate, findDepartment);
		TeacherCreateCommandHandler handler = new TeacherCreateCommandHandler(teachers, departments);
		return TeacherResponse.FromResult(await handler.Handle(command));
	}
}
