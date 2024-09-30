using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Services;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans;

namespace SPerfomance.Application.EducationPlans.Module.Queries.Count;

public sealed class EducationPlansGetCountService(IRepository<EducationPlan> repository) : IService<int>
{
	private readonly IRepository<EducationPlan> _repository = repository;
	public async Task<OperationResult<int>> DoOperation()
	{
		int count = await _repository.Count();
		return new OperationResult<int>(count);
	}
}
