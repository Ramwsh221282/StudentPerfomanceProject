using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Queries;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.Teachers;

namespace SPerfomance.Application.Teachers.Module.Queries.Count;

public sealed class GetTeachersCountQuery(IRepository<Teacher> repository) : IQuery
{
	public readonly IQueryHandler<GetTeachersCountQuery, int> Handler = new QueryHandler(repository);
	internal sealed class QueryHandler(IRepository<Teacher> repository) : IQueryHandler<GetTeachersCountQuery, int>
	{
		private readonly IRepository<Teacher> _repository = repository;

		public async Task<OperationResult<int>> Handle(GetTeachersCountQuery query)
		{
			int count = await _repository.Count();
			return new OperationResult<int>(count);
		}
	}
}
