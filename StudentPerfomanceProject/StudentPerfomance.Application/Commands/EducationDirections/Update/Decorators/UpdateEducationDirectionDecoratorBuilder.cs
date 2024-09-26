using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Commands.EducationDirections.Update.Decorators;

internal sealed class UpdateEducationDirectionDecoratorBuilder
{
	private readonly IRepository<EducationDirection> _repository;
	private readonly UpdateEducationDirectionCommand _command;
	private ICommandHandler<UpdateEducationDirectionCommand, OperationResult<EducationDirection>> _handler;

	public UpdateEducationDirectionDecoratorBuilder(IRepository<EducationDirection> repository, UpdateEducationDirectionCommand command)
	{
		_repository = repository;
		_command = command;
		_handler = new UpdateEducationDirectionDefaultHandler(repository);
	}

	public ICommandHandler<UpdateEducationDirectionCommand, OperationResult<EducationDirection>> Build()
	{
		BuildWithCodeChange();
		BuildWithNameChange();
		return _handler;
	}

	private void BuildWithCodeChange()
	{
		if (_command.OldSchema.Code != _command.NewSchema.Code)
			_handler = new UpdateEducationDirectionCodeHandler(_handler, _repository);
	}

	private void BuildWithNameChange()
	{
		if (_command.OldSchema.Name != _command.NewSchema.Name)
			_handler = new UpdateEducationDirectionNameHandler(_handler);
	}
}
