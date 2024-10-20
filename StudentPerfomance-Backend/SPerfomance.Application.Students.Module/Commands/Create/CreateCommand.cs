using CSharpFunctionalExtensions;

using SPerfomance.Application.Shared.Module.CQRS.Commands;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas;
using SPerfomance.Application.Shared.Module.Schemas.Students;
using SPerfomance.Application.Shared.Module.Schemas.Students.Validators;
using SPerfomance.Application.Shared.Users.Module.Commands.Common;
using SPerfomance.Application.Students.Module.Repository;
using SPerfomance.Application.Students.Module.Repository.Expressions;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups;
using SPerfomance.Domain.Module.Shared.Entities.Students;
using SPerfomance.Domain.Module.Shared.Entities.Users;

namespace SPerfomance.Application.Students.Module.Commands.Create;

internal sealed class CreateCommand : ICommand
{
	private readonly StudentSchema _student;
	private readonly IRepositoryExpression<StudentGroup> _getGroup;
	private readonly IRepositoryExpression<Student> _findDublicate;
	private readonly StudentCommandRepository _repository;
	private readonly ISchemaValidator _validator;
	public ICommandHandler<CreateCommand, Student> Handler;

	public CreateCommand(StudentSchema student, string token)
	{
		_student = student;
		_getGroup = ExpressionsFactory.GetGroupByName(student.ToRepositoryObject());
		_findDublicate = ExpressionsFactory.GetByRecordbook(student.ToRepositoryObject());
		_repository = new StudentCommandRepository();
		_validator = new StudentValidator()
		.WithNameValidation(student)
		.WithStateValidation(student)
		.WithRecordbookValidation(student);
		_validator.ProcessValidation();
		Handler = new VerificationHandler<CreateCommand, Student>(token, User.Admin);
		Handler = new CommandHandler(Handler, _repository);
	}

	internal sealed class CommandHandler : DecoratedCommandHandler<CreateCommand, Student>
	{
		private readonly StudentCommandRepository _repository;

		public CommandHandler(
			ICommandHandler<CreateCommand, Student> handler,
			StudentCommandRepository repository) : base(handler)
		{
			_repository = repository;
		}

		public override async Task<OperationResult<Student>> Handle(CreateCommand command)
		{
			var result = await base.Handle(command);
			if (result.IsFailed)
				return result;

			if (!command._validator.IsValid)
				return new OperationResult<Student>(command._validator.Error);

			Result<Student> create = await _repository.Create(command._student, command._getGroup, command._findDublicate);
			return create.IsFailure ?
				new OperationResult<Student>(create.Error) :
				new OperationResult<Student>(create.Value);
		}
	}
}
