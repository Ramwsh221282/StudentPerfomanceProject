using SPerfomance.Application.Shared.Module.CQRS.Queries;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.StudentGroups;
using SPerfomance.Application.StudentGroups.Module.Repository;
using SPerfomance.Application.StudentGroups.Module.Repository.Expressions;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups;

namespace SPerfomance.Application.StudentGroups.Module.Queries.Search;

internal sealed class SearchQuery : IQuery
{
	private readonly IRepositoryExpression<StudentGroup> _expression;
	private readonly StudentGroupQueryRepository _repository;
	public readonly IQueryHandler<SearchQuery, IReadOnlyCollection<StudentGroup>> Handler;
	public SearchQuery(StudentsGroupSchema group)
	{
		_expression = ExpressionsFactory.Filter(group.ToRepositoryObject());
		_repository = new StudentGroupQueryRepository();
		Handler = new QueryHandler(_repository);
	}

	internal sealed class QueryHandler(StudentGroupQueryRepository repository) : IQueryHandler<SearchQuery, IReadOnlyCollection<StudentGroup>>
	{
		private readonly StudentGroupQueryRepository _repository = repository;

		public async Task<OperationResult<IReadOnlyCollection<StudentGroup>>> Handle(SearchQuery query)
		{
			IReadOnlyCollection<StudentGroup> groups = await _repository.GetFiltered(query._expression);
			return new OperationResult<IReadOnlyCollection<StudentGroup>>(groups);
		}
	}
}
