using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Services;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments;

namespace SPerfomance.Application.TeacherDepartments.Module.Queries.Paged;

public sealed class TeachersDepartmentPaginationService
(
	int page,
	int pageSize,
	IRepository<TeachersDepartment> repository
) : IService<IReadOnlyCollection<TeachersDepartment>>
{
	private readonly int _page = page;
	private readonly int _pageSize = pageSize;
	private readonly IRepository<TeachersDepartment> _repository = repository;
	public async Task<OperationResult<IReadOnlyCollection<TeachersDepartment>>> DoOperation()
	{
		IReadOnlyCollection<TeachersDepartment> departments = await _repository.GetPaged(_page, _pageSize);
		return new OperationResult<IReadOnlyCollection<TeachersDepartment>>(departments);
	}
}
