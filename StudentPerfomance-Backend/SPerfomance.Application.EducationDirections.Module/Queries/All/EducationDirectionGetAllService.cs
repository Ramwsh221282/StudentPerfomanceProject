using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Services;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;

namespace SPerfomance.Application.EducationDirections.Module.Queries.All;

public sealed class EducationDirectionGetAllService(IRepository<EducationDirection> repository) : IService<IReadOnlyCollection<EducationDirection>>
{
	private readonly IRepository<EducationDirection> _repository = repository;
	public async Task<OperationResult<IReadOnlyCollection<EducationDirection>>> DoOperation()
	{
		IReadOnlyCollection<EducationDirection> directions = await _repository.GetAll();
		return new OperationResult<IReadOnlyCollection<EducationDirection>>(directions);
	}
}
