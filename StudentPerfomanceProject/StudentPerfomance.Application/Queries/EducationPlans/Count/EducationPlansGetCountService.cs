using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Queries.EducationPlans.Count;

public sealed class EducationPlansGetCountService(IRepository<EducationPlan> repository) : IService<int>
{
	private readonly IRepository<EducationPlan> _repository = repository;
	public async Task<OperationResult<int>> DoOperation()
	{
		int count = await _repository.Count();
		return new OperationResult<int>(count);
	}
}
