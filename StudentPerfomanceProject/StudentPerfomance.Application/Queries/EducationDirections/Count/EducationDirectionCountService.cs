using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Queries.EducationDirections.Count;

public sealed class EducationDirectionCountService(IRepository<EducationDirection> repository) : IService<int>
{
	private readonly IRepository<EducationDirection> _repository = repository;
	public async Task<OperationResult<int>> DoOperation()
	{
		int count = await _repository.Count();
		return new OperationResult<int>(count);
	}
}
