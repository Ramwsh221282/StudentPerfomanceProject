using CSharpFunctionalExtensions;

using SPerfomance.Application.Shared.Module.CQRS.Commands;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas;
using SPerfomance.Application.Shared.Module.Schemas.Students;
using SPerfomance.Application.Shared.Module.Schemas.Students.Validators;
using SPerfomance.Application.Students.Module.Repository;
using SPerfomance.Application.Students.Module.Repository.Expressions;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups;
using SPerfomance.Domain.Module.Shared.Entities.Students;

namespace SPerfomance.Application.Students.Module.Commands.Create;

internal sealed class CreateCommand : ICommand
{
	private readonly StudentSchema _student;
	private readonly IRepositoryExpression<StudentGroup> _getGroup;
	private readonly IRepositoryExpression<Student> _findDublicate;
	private readonly StudentCommandRepository _repository;
	private readonly ISchemaValidator _validator;
	public ICommandHandler<CreateCommand, Student> Handler;

	public CreateCommand(StudentSchema student)
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
		Handler = new CommandHandler(_repository);
	}

	internal sealed class CommandHandler(StudentCommandRepository repository) : ICommandHandler<CreateCommand, Student>
	{
		private readonly StudentCommandRepository _repository = repository;

		public async Task<OperationResult<Student>> Handle(CreateCommand command)
		{
			if (!command._validator.IsValid) return new OperationResult<Student>(command._validator.Error);
			Result<Student> create = await _repository.Create(command._student, command._getGroup, command._findDublicate);
			return create.IsFailure ?
				new OperationResult<Student>(create.Error) :
				new OperationResult<Student>(create.Value);
		}
	}
}
