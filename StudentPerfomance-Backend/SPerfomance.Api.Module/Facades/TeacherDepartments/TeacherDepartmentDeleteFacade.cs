using Microsoft.AspNetCore.Mvc;

using SPerfomance.Api.Module.Converters.TeacherDepartments;
using SPerfomance.Api.Module.Responses.TeacherDepartments;
using SPerfomance.Application.Shared.Module.Schemas.Departments;
using SPerfomance.Application.TeacherDepartments.Module.Commands.Delete;
using SPerfomance.DataAccess.Module.Shared;
using SPerfomance.DataAccess.Module.Shared.Repositories.TeachersDepartments;
using SPerfomance.DataAccess.Module.Shared.Repositories.TeachersDepartments.Expressions;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Services;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments;

namespace SPerfomance.Api.Module.Facades.TeacherDepartments;

internal sealed class TeacherDepartmentDeleteFacade(DepartmentSchema schema) : IFacade<TeacherDepartmentResponse>
{
	private readonly DepartmentSchema _schema = schema;
	public async Task<ActionResult<TeacherDepartmentResponse>> Process()
	{
		DepartmentRepositoryObject parameter = _schema.ToRepositoryObject();
		IRepositoryExpression<TeachersDepartment> expression = TeachersDepartmentsExpressionsFactory.HasDepartment(parameter);
		IRepository<TeachersDepartment> repository = RepositoryProvider.CreateDepartmentsRepository();
		IService<TeachersDepartment> service = new TeacherDepartmentDeletionService(expression, repository);
		return TeacherDepartmentResponse.FromResult(await service.DoOperation());
	}
}
