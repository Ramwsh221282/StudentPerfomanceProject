using SPerfomance.Application.EducationDirections.Module.Commands.Update.Decorators;
using SPerfomance.Application.Shared.Module.Schemas.EducationDirections;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Commands;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Services;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;

namespace SPerfomance.Application.EducationDirections.Module.Commands.Update;

public sealed class EducationDirectionUpdateService
(
	EducationDirectionSchema newSchema,
	IRepositoryExpression<EducationDirection> findDirection,
	IRepositoryExpression<EducationDirection> checkForCodeDublicate,
	IRepository<EducationDirection> repository
) : IService<EducationDirection>
{
	private readonly UpdateEducationDirectionCommand _command = new UpdateEducationDirectionCommand(newSchema, findDirection, checkForCodeDublicate);
	private readonly IRepository<EducationDirection> _repository = repository;
	public async Task<OperationResult<EducationDirection>> DoOperation()
	{
		UpdateEducationDirectionDecoratorBuilder builder = new UpdateEducationDirectionDecoratorBuilder(_repository);
		ICommandHandler<UpdateEducationDirectionCommand, OperationResult<EducationDirection>> handler = builder.Build();
		OperationResult<EducationDirection> result = await handler.Handle(_command);
		if (result.Result != null && !result.IsFailed && _repository is IForceUpdatableRepository<EducationDirection> forceUpdatable)
			await forceUpdatable.ForceUpdate(result.Result);
		return result;
	}
}
