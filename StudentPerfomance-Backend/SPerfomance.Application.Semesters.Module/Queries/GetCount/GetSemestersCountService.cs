using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Services;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.Semesters;

namespace SPerfomance.Application.Semesters.Module.Queries.GetCount;

public sealed class GetSemestersCountService(IRepository<Semester> repository) : IService<int>
{
	private readonly IRepository<Semester> _repository = repository;
	public async Task<OperationResult<int>> DoOperation()
	{
		int count = await _repository.Count();
		return new OperationResult<int>(count);
	}
}
