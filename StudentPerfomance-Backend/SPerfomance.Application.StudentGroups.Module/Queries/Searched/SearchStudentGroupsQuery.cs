using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Queries;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups;

namespace SPerfomance.Application.StudentGroups.Module.Queries.Searched;

public sealed class SearchStudentGroupsQuery(IRepositoryExpression<StudentGroup> expression, IRepository<StudentGroup> repository) : IQuery
{
	private readonly IRepositoryExpression<StudentGroup> _expression = expression;
	public IQueryHandler<SearchStudentGroupsQuery, IReadOnlyCollection<StudentGroup>> Handler { get; init; } = new QueryHandler(repository);
	internal sealed class QueryHandler(IRepository<StudentGroup> repository) : IQueryHandler<SearchStudentGroupsQuery, IReadOnlyCollection<StudentGroup>>
	{
		private readonly IRepository<StudentGroup> _repository = repository;
		public async Task<OperationResult<IReadOnlyCollection<StudentGroup>>> Handle(SearchStudentGroupsQuery query)
		{
			IReadOnlyCollection<StudentGroup> groups = await _repository.GetFiltered(query._expression);
			return new OperationResult<IReadOnlyCollection<StudentGroup>>(groups);
		}
	}
}
