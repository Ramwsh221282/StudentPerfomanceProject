using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Services;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments;

namespace SPerfomance.Application.TeacherDepartments.Module.Queries.Filtered;

public sealed class TeachersDepartmentFilterService
(
	int page,
	int pageSize,
	IRepositoryExpression<TeachersDepartment> expression,
	IRepository<TeachersDepartment> repository
) : IService<IReadOnlyCollection<TeachersDepartment>>
{
	private readonly int _page = page;
	private readonly int _pageSize = pageSize;
	private readonly IRepositoryExpression<TeachersDepartment> _expression = expression;
	private readonly IRepository<TeachersDepartment> _repository = repository;
	public async Task<OperationResult<IReadOnlyCollection<TeachersDepartment>>> DoOperation()
	{
		IReadOnlyCollection<TeachersDepartment> departments = await _repository.GetFilteredAndPaged(_expression, _page, _pageSize);
		return new OperationResult<IReadOnlyCollection<TeachersDepartment>>(departments);
	}
}
