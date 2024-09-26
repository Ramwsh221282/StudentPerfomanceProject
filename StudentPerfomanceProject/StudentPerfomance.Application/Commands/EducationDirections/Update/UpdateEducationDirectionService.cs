using StudentPerfomance.Application.EntitySchemas.Schemas.EducationDirections;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Commands.EducationDirections.Update.Decorators;

public sealed class UpdateEducationDirectionService
(
	EducationDirectionSchema oldSchema,
	EducationDirectionSchema newSchema,
	IRepositoryExpression<EducationDirection> findDirection,
	IRepositoryExpression<EducationDirection> checkForCodeDublicate,
	IRepository<EducationDirection> repository
) : IService<EducationDirection>
{
	private readonly UpdateEducationDirectionCommand _command = new UpdateEducationDirectionCommand(oldSchema, newSchema, findDirection, checkForCodeDublicate);
	private readonly IRepository<EducationDirection> _repository = repository;
	public async Task<OperationResult<EducationDirection>> DoOperation()
	{
		ICommandHandler<UpdateEducationDirectionCommand, OperationResult<EducationDirection>> defaultHandler = new UpdateEducationDirectionDefaultHandler(_repository);
		ICommandHandler<UpdateEducationDirectionCommand, OperationResult<EducationDirection>> codeUpdate = new UpdateEducationDirectionCodeHandler(defaultHandler, _repository);
		ICommandHandler<UpdateEducationDirectionCommand, OperationResult<EducationDirection>> nameUpdate = new UpdateEducationDirectionNameHandler(codeUpdate);
		OperationResult<EducationDirection> result = await nameUpdate.Handle(_command);
		if (result.Result != null && !result.IsFailed && _repository is IForceUpdatableRepository<EducationDirection> forceUpdatable)
			await forceUpdatable.ForceUpdate(result.Result);
		return result;
	}
}
