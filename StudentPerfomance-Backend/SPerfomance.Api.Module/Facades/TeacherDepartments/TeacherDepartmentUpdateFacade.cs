using Microsoft.AspNetCore.Mvc;

using SPerfomance.Api.Module.Converters.TeacherDepartments;
using SPerfomance.Api.Module.Responses.TeacherDepartments;
using SPerfomance.Application.Shared.Module.Schemas.Departments;
using SPerfomance.Application.TeacherDepartments.Module.Commands.Update;
using SPerfomance.DataAccess.Module.Shared;
using SPerfomance.DataAccess.Module.Shared.Repositories.TeachersDepartments;
using SPerfomance.DataAccess.Module.Shared.Repositories.TeachersDepartments.Expressions;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Services;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments;

namespace SPerfomance.Api.Module.Facades.TeacherDepartments;

internal sealed class TeacherDepartmentUpdateFacade(DepartmentSchema oldSchema, DepartmentSchema newSchema) : IFacade<TeacherDepartmentResponse>
{
	private readonly DepartmentSchema _oldDepartment = oldSchema;
	private readonly DepartmentSchema _newSchema = newSchema;
	public async Task<ActionResult<TeacherDepartmentResponse>> Process()
	{
		DepartmentRepositoryObject oldDepartment = _oldDepartment.ToRepositoryObject();
		DepartmentRepositoryObject newDepartment = _newSchema.ToRepositoryObject();
		IRepositoryExpression<TeachersDepartment> findInitial = TeachersDepartmentsExpressionsFactory.HasDepartment(oldDepartment);
		IRepositoryExpression<TeachersDepartment> findNameDublicate = TeachersDepartmentsExpressionsFactory.FindByName(newDepartment);
		IRepository<TeachersDepartment> repository = RepositoryProvider.CreateDepartmentsRepository();
		IService<TeachersDepartment> service = new TeachersDepartmentUpdateService(_newSchema, findInitial, findNameDublicate, repository);
		return TeacherDepartmentResponse.FromResult(await service.DoOperation());
	}
}
