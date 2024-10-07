using CSharpFunctionalExtensions;

using SPerfomance.Application.Shared.Module.CQRS.Commands;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas;
using SPerfomance.Application.Shared.Module.Schemas.StudentGroups;
using SPerfomance.Application.Shared.Module.Schemas.StudentGroups.Validators;
using SPerfomance.Application.StudentGroups.Module.Repository;
using SPerfomance.Application.StudentGroups.Module.Repository.Expressions;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups;

namespace SPerfomance.Application.StudentGroups.Module.Commands.Create;

internal sealed class CreateCommand : ICommand
{
	private readonly StudentsGroupSchema _group;
	private readonly IRepositoryExpression<StudentGroup> _findDublicate;
	private readonly StudentGroupCommandRepository _repository;
	private readonly ISchemaValidator _validator;
	public readonly ICommandHandler<CreateCommand, StudentGroup> Handler;
	public CreateCommand(StudentsGroupSchema group)
	{
		_group = group;
		_findDublicate = ExpressionsFactory.GetByName(_group.ToRepositoryObject());
		_repository = new StudentGroupCommandRepository();
		_validator = new StudentGroupSchemaValidator()
		.WithNameValidation(group);
		_validator.ProcessValidation();
		Handler = new CommandHandler(_repository);
	}

	internal sealed class CommandHandler(StudentGroupCommandRepository repository) : ICommandHandler<CreateCommand, StudentGroup>
	{
		private readonly StudentGroupCommandRepository _repository = repository;
		public async Task<OperationResult<StudentGroup>> Handle(CreateCommand command)
		{
			if (!command._validator.IsValid) return new OperationResult<StudentGroup>(command._validator.Error);
			Result<StudentGroup> create = await _repository.Create(command._group, command._findDublicate);
			return create.IsFailure ?
				new OperationResult<StudentGroup>(create.Error) :
				new OperationResult<StudentGroup>(create.Value);
		}
	}
}
