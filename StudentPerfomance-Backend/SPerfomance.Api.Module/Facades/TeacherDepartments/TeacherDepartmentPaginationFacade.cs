using Microsoft.AspNetCore.Mvc;

using SPerfomance.Api.Module.Responses.TeacherDepartments;
using SPerfomance.Application.TeacherDepartments.Module.Queries.Paged;
using SPerfomance.DataAccess.Module.Shared;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Services;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments;

namespace SPerfomance.Api.Module.Facades.TeacherDepartments;

internal sealed class TeacherDepartmentPaginationFacade(int page, int pageSize) : IFacade<IReadOnlyCollection<TeacherDepartmentResponse>>
{
	private readonly int _page = page;
	private readonly int _pageSize = pageSize;
	public async Task<ActionResult<IReadOnlyCollection<TeacherDepartmentResponse>>> Process()
	{
		IRepository<TeachersDepartment> repository = RepositoryProvider.CreateDepartmentsRepository();
		IService<IReadOnlyCollection<TeachersDepartment>> service = new TeachersDepartmentPaginationService(_page, _pageSize, repository);
		return TeacherDepartmentResponse.FromResult(await service.DoOperation());
	}
}
