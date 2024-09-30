using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Commands;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;

namespace SPerfomance.Application.EducationDirections.Module.Commands.Update.Decorators;

internal sealed class UpdateEducationDirectionDecoratorBuilder
{
	private readonly IRepository<EducationDirection> _repository;
	private ICommandHandler<UpdateEducationDirectionCommand, OperationResult<EducationDirection>> _handler;
	public UpdateEducationDirectionDecoratorBuilder(IRepository<EducationDirection> repository)
	{
		_repository = repository;
		_handler = new UpdateEducationDirectionDefaultHandler(repository);
	}
	public ICommandHandler<UpdateEducationDirectionCommand, OperationResult<EducationDirection>> Build()
	{
		BuildWithCodeChange();
		BuildWithNameChange();
		return _handler;
	}
	private void BuildWithCodeChange() => _handler = new UpdateEducationDirectionCodeHandler(_handler, _repository);
	private void BuildWithNameChange() => _handler = new UpdateEducationDirectionNameHandler(_handler);
}
