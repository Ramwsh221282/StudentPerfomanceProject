using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Services;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;

namespace SPerfomance.Application.EducationDirections.Module.Queries.GetFiltered;

public sealed class EducationDirectionGetByFilter
(
	IRepository<EducationDirection> repository,
	IRepositoryExpression<EducationDirection> expression
) : IService<IReadOnlyCollection<EducationDirection>>
{
	private readonly IRepository<EducationDirection> _repository = repository;
	private readonly IRepositoryExpression<EducationDirection> _expression = expression;
	public async Task<OperationResult<IReadOnlyCollection<EducationDirection>>> DoOperation()
	{
		IReadOnlyCollection<EducationDirection> directions = await _repository.GetFiltered(_expression);
		return new OperationResult<IReadOnlyCollection<EducationDirection>>(directions);
	}
}
