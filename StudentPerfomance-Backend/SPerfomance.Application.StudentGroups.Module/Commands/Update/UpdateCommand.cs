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
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups.Errors;
using SPerfomance.Domain.Module.Shared.Entities.Users;

namespace SPerfomance.Application.StudentGroups.Module.Commands.Update;

internal sealed class UpdateCommand : ICommand
{
	private readonly StudentsGroupSchema _newSchema;
	private readonly IRepositoryExpression<StudentGroup> _getInitial;
	private readonly IRepositoryExpression<StudentGroup> _findDublicate;
	private readonly StudentGroupQueryRepository _repository;
	private readonly ISchemaValidator _validator;
	public readonly ICommandHandler<UpdateCommand, StudentGroup> Handler;
	public UpdateCommand(StudentsGroupSchema oldSchema, StudentsGroupSchema newSchema, string token)
	{
		_newSchema = newSchema;
		_getInitial = ExpressionsFactory.GetByName(oldSchema.ToRepositoryObject());
		_findDublicate = ExpressionsFactory.GetByName(newSchema.ToRepositoryObject());
		_repository = new StudentGroupQueryRepository();
		_validator = new StudentGroupSchemaValidator().WithNameValidation(newSchema);
		_validator.ProcessValidation();
		Handler = new VerificationHandler<UpdateCommand, StudentGroup>(token, User.Admin);
		Handler = new CommandHandler(Handler, _repository);
	}

	internal sealed class CommandHandler : DecoratedCommandHandler<UpdateCommand, StudentGroup>
	{
		private readonly StudentGroupQueryRepository _repository;

		public CommandHandler(
			ICommandHandler<UpdateCommand, StudentGroup> handler,
			StudentGroupQueryRepository repository)
			: base(handler)
		{
			_repository = repository;
		}

		public override async Task<OperationResult<StudentGroup>> Handle(UpdateCommand command)
		{
			var result = await base.Handle(command);
			if (result.IsFailed)
				return result;

			if (!command._validator.IsValid)
				return new OperationResult<StudentGroup>(command._validator.Error);
			if (await _repository.HasEqualRecord(command._findDublicate))
				return new OperationResult<StudentGroup>(new GroupDublicateNameError(command._newSchema.Name).ToString());
			StudentGroup? group = await _repository.GetByParameter(command._getInitial);
			if (group == null)
				return new OperationResult<StudentGroup>(new GroupNotFoundError().ToString());
			if (group.Name.Name != command._newSchema.Name)
			{
				group.ChangeGroupName(command._newSchema.CreateGroupName());
				await _repository.Commit();
			}
			return new OperationResult<StudentGroup>(group);
		}
	}
}
