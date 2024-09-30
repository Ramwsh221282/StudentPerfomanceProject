using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Services;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.Teachers;

namespace SPerfomance.Application.Teachers.Module.Queries.Count;

public sealed class GetTeachersCountService(IRepository<Teacher> repository) : IService<int>
{
	private readonly IRepository<Teacher> _repository = repository;
	public async Task<OperationResult<int>> DoOperation()
	{
		int count = await _repository.Count();
		return new OperationResult<int>(count);
	}
}
