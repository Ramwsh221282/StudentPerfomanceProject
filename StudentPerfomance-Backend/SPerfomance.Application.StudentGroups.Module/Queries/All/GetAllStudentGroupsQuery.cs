using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Queries;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups;

namespace SPerfomance.Application.StudentGroups.Module.Queries.All;

public sealed class GetAllStudentGroupsQuery(IRepository<StudentGroup> repository) : IQuery
{
	public IQueryHandler<GetAllStudentGroupsQuery, IReadOnlyCollection<StudentGroup>> Handler { get; init; } = new QueryHandler(repository);
	internal sealed class QueryHandler(IRepository<StudentGroup> repository) : IQueryHandler<GetAllStudentGroupsQuery, IReadOnlyCollection<StudentGroup>>
	{
		private readonly IRepository<StudentGroup> _repository = repository;
		public async Task<OperationResult<IReadOnlyCollection<StudentGroup>>> Handle(GetAllStudentGroupsQuery query)
		{
			IReadOnlyCollection<StudentGroup> groups = await _repository.GetAll();
			return new OperationResult<IReadOnlyCollection<StudentGroup>>(groups);
		}
	}
}
