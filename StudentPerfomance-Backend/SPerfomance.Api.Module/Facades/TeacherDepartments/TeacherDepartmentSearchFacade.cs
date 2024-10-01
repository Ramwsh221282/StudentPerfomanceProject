using Microsoft.AspNetCore.Mvc;

using SPerfomance.Api.Module.Converters.TeacherDepartments;
using SPerfomance.Api.Module.Responses.TeacherDepartments;
using SPerfomance.Application.Shared.Module.Schemas.Departments;
using SPerfomance.Application.TeacherDepartments.Module.Queries.Searched;
using SPerfomance.DataAccess.Module.Shared;
using SPerfomance.DataAccess.Module.Shared.Repositories.TeachersDepartments;
using SPerfomance.DataAccess.Module.Shared.Repositories.TeachersDepartments.Expressions;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Services;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments;

namespace SPerfomance.Api.Module.Facades.TeacherDepartments;

internal sealed class TeacherDepartmentSearchFacade(DepartmentSchema schema) : IFacade<IReadOnlyCollection<TeacherDepartmentResponse>>
{
	private readonly DepartmentSchema _schema = schema;
	public async Task<ActionResult<IReadOnlyCollection<TeacherDepartmentResponse>>> Process()
	{
		DepartmentRepositoryObject parameter = _schema.ToRepositoryObject();
		IRepositoryExpression<TeachersDepartment> expression = TeachersDepartmentsExpressionsFactory.Filter(parameter);
		IRepository<TeachersDepartment> repository = RepositoryProvider.CreateDepartmentsRepository();
		IService<IReadOnlyCollection<TeachersDepartment>> service = new TeachersDepartmentSearchService(expression, repository);
		return TeacherDepartmentResponse.FromResult(await service.DoOperation());
	}
}
