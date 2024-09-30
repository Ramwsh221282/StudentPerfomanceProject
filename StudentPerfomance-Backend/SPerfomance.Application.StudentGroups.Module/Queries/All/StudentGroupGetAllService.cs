using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Services;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups;

namespace SPerfomance.Application.StudentGroups.Module.Queries.All;

public sealed class StudentGroupGetAllService(IRepository<StudentGroup> repository) : IService<IReadOnlyCollection<StudentGroup>>
{
	private readonly IRepository<StudentGroup> _repository = repository;
	public async Task<OperationResult<IReadOnlyCollection<StudentGroup>>> DoOperation()
	{
		IReadOnlyCollection<StudentGroup> groups = await _repository.GetAll();
		return new OperationResult<IReadOnlyCollection<StudentGroup>>(groups);
	}
}
