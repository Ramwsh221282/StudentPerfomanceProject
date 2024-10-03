using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Queries;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.Students;

namespace SPerfomance.Application.Students.Module.Queries.Count;

public sealed class GetStudentsCountQuery(IRepository<Student> repository) : IQuery
{
	public readonly IQueryHandler<GetStudentsCountQuery, int> Handler = new QueryHandler(repository);
	internal sealed class QueryHandler(IRepository<Student> repository) : IQueryHandler<GetStudentsCountQuery, int>
	{
		private readonly IRepository<Student> _repository = repository;
		public async Task<OperationResult<int>> Handle(GetStudentsCountQuery query)
		{
			int count = await _repository.Count();
			return new OperationResult<int>(count);
		}
	}
}
