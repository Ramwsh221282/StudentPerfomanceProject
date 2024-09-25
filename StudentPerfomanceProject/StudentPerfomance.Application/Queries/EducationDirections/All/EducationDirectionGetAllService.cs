using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Queries.EducationDirections.All;

public sealed class EducationDirectionGetAllService(IRepository<EducationDirection> repository) : IService<IReadOnlyCollection<EducationDirection>>
{
	private readonly IRepository<EducationDirection> _repository = repository;
	public async Task<OperationResult<IReadOnlyCollection<EducationDirection>>> DoOperation()
	{
		IReadOnlyCollection<EducationDirection> directions = await _repository.GetAll();
		return new OperationResult<IReadOnlyCollection<EducationDirection>>(directions);
	}
}
