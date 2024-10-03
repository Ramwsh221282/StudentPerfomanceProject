using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Queries;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.Semesters;

namespace SPerfomance.Application.Semesters.Module.Queries.GetCount;

public sealed class GetSemestersCountQuery(IRepository<Semester> repository) : IQuery
{
	public readonly IQueryHandler<GetSemestersCountQuery, int> Handler = new QueryHandler(repository);
	internal sealed class QueryHandler(IRepository<Semester> repository) : IQueryHandler<GetSemestersCountQuery, int>
	{
		private readonly IRepository<Semester> _repository = repository;

		public async Task<OperationResult<int>> Handle(GetSemestersCountQuery query)
		{
			int count = await _repository.Count();
			return new OperationResult<int>(count);
		}
	}
}
