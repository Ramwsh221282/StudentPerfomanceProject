using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Services;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans;

namespace SPerfomance.Application.EducationPlans.Module.Queries.ByPage;

public sealed class EducationPlansGetPagedService(int page, int pageSize, IRepository<EducationPlan> repository) : IService<IReadOnlyCollection<EducationPlan>>
{
	private readonly IRepository<EducationPlan> _repository = repository;
	public async Task<OperationResult<IReadOnlyCollection<EducationPlan>>> DoOperation()
	{
		IReadOnlyCollection<EducationPlan> plans = await _repository.GetPaged(page, pageSize);
		return new OperationResult<IReadOnlyCollection<EducationPlan>>(plans);
	}
}
