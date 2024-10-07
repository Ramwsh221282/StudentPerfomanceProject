using SPerfomance.Application.Shared.Module.CQRS.Queries;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.Teachers;
using SPerfomance.Application.Teachers.Module.Repository;
using SPerfomance.Application.Teachers.Module.Repository.Expressions;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.Teachers;

namespace SPerfomance.Application.Teachers.Module.Queries.Searched;

internal sealed class SearchQuery : IQuery
{
	private readonly IRepositoryExpression<Teacher> _expression;
	private readonly TeacherQueryRepository _repository;
	public readonly IQueryHandler<SearchQuery, IReadOnlyCollection<Teacher>> Handler;
	public SearchQuery(TeacherSchema teacher)
	{
		_expression = ExpressionsFactory.Filter(teacher.ToRepositoryObject());
		_repository = new TeacherQueryRepository();
		Handler = new QueryHandler(_repository);
	}

	internal sealed class QueryHandler(TeacherQueryRepository repository) : IQueryHandler<SearchQuery, IReadOnlyCollection<Teacher>>
	{
		private readonly TeacherQueryRepository _repository = repository;

		public async Task<OperationResult<IReadOnlyCollection<Teacher>>> Handle(SearchQuery query)
		{
			IReadOnlyCollection<Teacher> teachers = await _repository.GetFiltered(query._expression);
			return new OperationResult<IReadOnlyCollection<Teacher>>(teachers);
		}
	}
}
