using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Services;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments;

namespace SPerfomance.Application.TeacherDepartments.Module.Queries.Searched;

public sealed class TeachersDepartmentSearchService
(
	IRepositoryExpression<TeachersDepartment> expression,
	IRepository<TeachersDepartment> repository
) : IService<IReadOnlyCollection<TeachersDepartment>>
{
	private readonly IRepositoryExpression<TeachersDepartment> _expression = expression;
	private readonly IRepository<TeachersDepartment> _repository = repository;
	public async Task<OperationResult<IReadOnlyCollection<TeachersDepartment>>> DoOperation()
	{
		IReadOnlyCollection<TeachersDepartment> departments = await _repository.GetFiltered(_expression);
		return new OperationResult<IReadOnlyCollection<TeachersDepartment>>(departments);
	}
}
