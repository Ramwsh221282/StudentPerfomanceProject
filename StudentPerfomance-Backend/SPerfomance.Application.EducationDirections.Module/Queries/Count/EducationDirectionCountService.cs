using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Services;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;

namespace SPerfomance.Application.EducationDirections.Module.Queries.Count;

public sealed class EducationDirectionCountService(IRepository<EducationDirection> repository) : IService<int>
{
	private readonly IRepository<EducationDirection> _repository = repository;
	public async Task<OperationResult<int>> DoOperation()
	{
		int count = await _repository.Count();
		return new OperationResult<int>(count);
	}
}
