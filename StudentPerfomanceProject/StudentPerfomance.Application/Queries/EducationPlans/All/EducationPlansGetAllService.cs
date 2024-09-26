using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Queries.EducationPlans.All;

public sealed class EducationPlansGetAllService(IRepository<EducationPlan> repository) : IService<IReadOnlyCollection<EducationPlan>>
{
	private readonly IRepository<EducationPlan> _repository = repository;
	public async Task<OperationResult<IReadOnlyCollection<EducationPlan>>> DoOperation()
	{
		IReadOnlyCollection<EducationPlan> plans = await _repository.GetAll();
		return new OperationResult<IReadOnlyCollection<EducationPlan>>(plans);
	}
}
