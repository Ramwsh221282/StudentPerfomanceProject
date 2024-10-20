using CSharpFunctionalExtensions;

using SPerfomance.Application.Shared.Module.CQRS.Commands;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas;
using SPerfomance.Application.Shared.Module.Schemas.StudentGroups;
using SPerfomance.Application.Shared.Module.Schemas.StudentGroups.Validators;
using SPerfomance.Application.Shared.Users.Module.Commands.Common;
using SPerfomance.Application.StudentGroups.Module.Repository;
using SPerfomance.Application.StudentGroups.Module.Repository.Expressions;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups;
using SPerfomance.Domain.Module.Shared.Entities.Users;

namespace SPerfomance.Application.StudentGroups.Module.Commands.Create;

internal sealed class CreateCommand : ICommand
{
	private readonly StudentsGroupSchema _group;
	private readonly IRepositoryExpression<StudentGroup> _findDublicate;
	private readonly StudentGroupCommandRepository _repository;
	private readonly ISchemaValidator _validator;

	public readonly ICommandHandler<CreateCommand, StudentGroup> Handler;

	public CreateCommand(StudentsGroupSchema group, string token)
	{
		_group = group;
		_findDublicate = ExpressionsFactory.GetByName(_group.ToRepositoryObject());
		_repository = new StudentGroupCommandRepository();
		_validator = new StudentGroupSchemaValidator()
		.WithNameValidation(group);
		_validator.ProcessValidation();
		Handler = new VerificationHandler<CreateCommand, StudentGroup>(token, User.Admin);
		Handler = new CommandHandler(Handler, _repository);
	}

	internal sealed class CommandHandler : DecoratedCommandHandler<CreateCommand, StudentGroup>
	{
		private readonly StudentGroupCommandRepository _repository;

		public CommandHandler(
			ICommandHandler<CreateCommand, StudentGroup> handler,
			StudentGroupCommandRepository repository)
			 : base(handler)
		{
			_repository = repository;
		}

		public override async Task<OperationResult<StudentGroup>> Handle(CreateCommand command)
		{
			var result = await base.Handle(command);
			if (result.IsFailed)
				return result;

			if (!command._validator.IsValid) return new OperationResult<StudentGroup>(command._validator.Error);
			Result<StudentGroup> create = await _repository.Create(command._group, command._findDublicate);
			return create.IsFailure ?
				new OperationResult<StudentGroup>(create.Error) :
				new OperationResult<StudentGroup>(create.Value);
		}
	}
}
