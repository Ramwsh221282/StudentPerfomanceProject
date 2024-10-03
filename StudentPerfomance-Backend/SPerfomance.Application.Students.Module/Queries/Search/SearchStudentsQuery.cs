using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Queries;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.Students;

namespace SPerfomance.Application.Students.Module.Queries.Search;

public sealed class SearchStudentsQuery(IRepositoryExpression<Student> expression, IRepository<Student> repository) : IQuery
{
	private readonly IRepositoryExpression<Student> _filter = expression;
	public readonly IQueryHandler<SearchStudentsQuery, IReadOnlyCollection<Student>> Handler = new QueryHandler(repository);
	internal sealed class QueryHandler(IRepository<Student> repository) : IQueryHandler<SearchStudentsQuery, IReadOnlyCollection<Student>>
	{
		private readonly IRepository<Student> _repository = repository;
		public async Task<OperationResult<IReadOnlyCollection<Student>>> Handle(SearchStudentsQuery query)
		{
			IReadOnlyCollection<Student> students = await _repository.GetFiltered(query._filter);
			return new OperationResult<IReadOnlyCollection<Student>>(students);
		}
	}
}
