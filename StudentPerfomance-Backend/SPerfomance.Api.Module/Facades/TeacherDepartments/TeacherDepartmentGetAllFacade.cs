using Microsoft.AspNetCore.Mvc;

using SPerfomance.Api.Module.Responses.TeacherDepartments;
using SPerfomance.Application.TeacherDepartments.Module.Queries.All;
using SPerfomance.DataAccess.Module.Shared;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Services;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments;

namespace SPerfomance.Api.Module.Facades.TeacherDepartments;

internal sealed class TeacherDepartmentGetAllFacade : IFacade<IReadOnlyCollection<TeacherDepartmentResponse>>
{
	public async Task<ActionResult<IReadOnlyCollection<TeacherDepartmentResponse>>> Process()
	{
		IRepository<TeachersDepartment> repository = RepositoryProvider.CreateDepartmentsRepository();
		IService<IReadOnlyCollection<TeachersDepartment>> service = new TeachersDepartmentGetAllService(repository);
		return TeacherDepartmentResponse.FromResult(await service.DoOperation());
	}
}
