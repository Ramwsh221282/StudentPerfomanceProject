using CSharpFunctionalExtensions;

using SPerfomance.Application.Shared.Module.CQRS.Commands;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas;
using SPerfomance.Application.Shared.Module.Schemas.Teachers;
using SPerfomance.Application.Shared.Module.Schemas.Teachers.Validators;
using SPerfomance.Application.Teachers.Module.Repository;
using SPerfomance.Application.Teachers.Module.Repository.Expressions;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments;
using SPerfomance.Domain.Module.Shared.Entities.Teachers;

namespace SPerfomance.Application.Teachers.Module.Commands.Create;

internal sealed class CreateCommand : ICommand
{
	private readonly TeacherSchema _teacher;
	private readonly IRepositoryExpression<Teacher> _findDublicate;
	private readonly IRepositoryExpression<TeachersDepartment> _getDepartment;
	private readonly TeacherCommandRepository _repository;
	private readonly ISchemaValidator _validator;
	public readonly ICommandHandler<CreateCommand, Teacher> Handler;
	public CreateCommand(TeacherSchema teacher)
	{
		_teacher = teacher;
		_findDublicate = ExpressionsFactory.GetTeacher(teacher.ToRepositoryObject());
		_getDepartment = ExpressionsFactory.GetDepartment(teacher.ToRepositoryObject());
		_repository = new TeacherCommandRepository();
		_validator = new TeacherValidator()
		.WithNameValidation(teacher)
		.WithJobTitle(teacher)
		.WithConditionValidation(teacher);
		_validator.ProcessValidation();
		Handler = new CommandHandler(_repository);
	}

	internal sealed class CommandHandler(TeacherCommandRepository repository) : ICommandHandler<CreateCommand, Teacher>
	{
		private readonly TeacherCommandRepository _repository = repository;

		public async Task<OperationResult<Teacher>> Handle(CreateCommand command)
		{
			if (!command._validator.IsValid) return new OperationResult<Teacher>(command._validator.Error);
			Result<Teacher> create = await _repository.Create(command._teacher, command._findDublicate, command._getDepartment);
			return create.IsFailure ?
				new OperationResult<Teacher>(create.Error) :
				new OperationResult<Teacher>(create.Value);
		}
	}
}
