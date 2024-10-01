using Microsoft.AspNetCore.Mvc;

using SPerfomance.Application.TeacherDepartments.Module.Queries.Count;
using SPerfomance.DataAccess.Module.Shared;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Services;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments;

namespace SPerfomance.Api.Module.Facades.TeacherDepartments;

internal sealed class TeacherDepartmentCountFacade : IFacade<int>
{
	public async Task<ActionResult<int>> Process()
	{
		IRepository<TeachersDepartment> repository = RepositoryProvider.CreateDepartmentsRepository();
		IService<int> service = new TeacherDepartmentsCountService(repository);
		OperationResult<int> result = await service.DoOperation();
		return new OkObjectResult(result.Result);
	}
}
