using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Queries.EducationPlans.ByPage;

public sealed class EducationPlansGetPagedService(int page, int pageSize, IRepository<EducationPlan> repository) : IService<IReadOnlyCollection<EducationPlan>>
{
	private readonly IRepository<EducationPlan> _repository = repository;
	public async Task<OperationResult<IReadOnlyCollection<EducationPlan>>> DoOperation()
	{
		IReadOnlyCollection<EducationPlan> plans = await _repository.GetPaged(page, pageSize);
		return new OperationResult<IReadOnlyCollection<EducationPlan>>(plans);
	}
}
