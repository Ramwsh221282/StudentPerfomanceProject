using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Queries;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments;

namespace SPerfomance.Application.TeacherDepartments.Module.Queries.Filtered;

public sealed class FilterTeacherDepartmentsQuery(int page, int pageSize, IRepositoryExpression<TeachersDepartment> expression, IRepository<TeachersDepartment> repository) : IQuery
{
	private readonly int _page = page;
	private readonly int _pageSize = pageSize;
	private readonly IRepositoryExpression<TeachersDepartment> _expression = expression;
	public readonly IQueryHandler<FilterTeacherDepartmentsQuery, IReadOnlyCollection<TeachersDepartment>> Handler = new QueryHandler(repository);
	internal sealed class QueryHandler(IRepository<TeachersDepartment> repository) : IQueryHandler<FilterTeacherDepartmentsQuery, IReadOnlyCollection<TeachersDepartment>>
	{
		private readonly IRepository<TeachersDepartment> _repository = repository;
		public async Task<OperationResult<IReadOnlyCollection<TeachersDepartment>>> Handle(FilterTeacherDepartmentsQuery query)
		{
			IReadOnlyCollection<TeachersDepartment> departments = await _repository.GetFilteredAndPaged(query._expression, query._page, query._pageSize);
			return new OperationResult<IReadOnlyCollection<TeachersDepartment>>(departments);
		}
	}
}
