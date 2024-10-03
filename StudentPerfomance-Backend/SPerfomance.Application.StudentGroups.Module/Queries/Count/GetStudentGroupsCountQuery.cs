using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Queries;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups;

namespace SPerfomance.Application.StudentGroups.Module.Queries.Count;

public sealed class GetStudentGroupsCountQuery(IRepository<StudentGroup> repository) : IQuery
{
	public IQueryHandler<GetStudentGroupsCountQuery, int> Handler { get; init; } = new QueryHandler(repository);
	internal sealed class QueryHandler(IRepository<StudentGroup> repository) : IQueryHandler<GetStudentGroupsCountQuery, int>
	{
		private readonly IRepository<StudentGroup> _repository = repository;
		public async Task<OperationResult<int>> Handle(GetStudentGroupsCountQuery query)
		{
			int count = await _repository.Count();
			return new OperationResult<int>(count);
		}
	}
}
