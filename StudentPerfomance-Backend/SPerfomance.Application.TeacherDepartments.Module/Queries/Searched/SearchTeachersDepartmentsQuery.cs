using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Queries;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments;

namespace SPerfomance.Application.TeacherDepartments.Module.Queries.Searched;

public sealed class SearchTeachersDepartmentsQuery(IRepositoryExpression<TeachersDepartment> expression, IRepository<TeachersDepartment> repository) : IQuery
{
	private readonly IRepositoryExpression<TeachersDepartment> _expression = expression;
	public readonly IQueryHandler<SearchTeachersDepartmentsQuery, IReadOnlyCollection<TeachersDepartment>> Handler = new QueryHandler(repository);
	internal sealed class QueryHandler(IRepository<TeachersDepartment> repository) : IQueryHandler<SearchTeachersDepartmentsQuery, IReadOnlyCollection<TeachersDepartment>>
	{
		private readonly IRepository<TeachersDepartment> _repository = repository;
		public async Task<OperationResult<IReadOnlyCollection<TeachersDepartment>>> Handle(SearchTeachersDepartmentsQuery query)
		{
			IReadOnlyCollection<TeachersDepartment> departments = await _repository.GetFiltered(query._expression);
			return new OperationResult<IReadOnlyCollection<TeachersDepartment>>(departments);
		}
	}
}
