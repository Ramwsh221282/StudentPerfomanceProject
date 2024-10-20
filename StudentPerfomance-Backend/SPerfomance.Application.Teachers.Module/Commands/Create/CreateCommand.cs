using CSharpFunctionalExtensions;

using SPerfomance.Application.Shared.Module.CQRS.Commands;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas;
using SPerfomance.Application.Shared.Module.Schemas.Departments;
using SPerfomance.Application.Shared.Module.Schemas.Teachers;
using SPerfomance.Application.Shared.Module.Schemas.Teachers.Validators;
using SPerfomance.Application.Shared.Users.Module.Commands.Common;
using SPerfomance.Application.Teachers.Module.Repository;
using SPerfomance.Application.Teachers.Module.Repository.Expressions;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments;
using SPerfomance.Domain.Module.Shared.Entities.Teachers;
using SPerfomance.Domain.Module.Shared.Entities.Users;

namespace SPerfomance.Application.Teachers.Module.Commands.Create;

internal sealed class CreateCommand : ICommand
{
	private readonly TeacherSchema _teacher;
	private readonly IRepositoryExpression<Teacher> _findDublicate;
	private readonly IRepositoryExpression<TeachersDepartment> _getDepartment;
	private readonly TeacherCommandRepository _repository;
	private readonly ISchemaValidator _validator;

	public readonly ICommandHandler<CreateCommand, Teacher> Handler;

	public CreateCommand(TeacherSchema teacher, string token)
	{
		_teacher = teacher;
		_findDublicate = ExpressionsFactory.GetTeacher(teacher.ToRepositoryObject());
		_getDepartment = ExpressionsFactory.GetDepartment(teacher.Department.ToRepositoryObject());
		_repository = new TeacherCommandRepository();
		_validator = new TeacherValidator()
		.WithNameValidation(teacher)
		.WithJobTitle(teacher)
		.WithConditionValidation(teacher);
		_validator.ProcessValidation();
		Handler = new VerificationHandler<CreateCommand, Teacher>(token, User.Admin);
		Handler = new CommandHandler(Handler, _repository);
	}

	internal sealed class CommandHandler : DecoratedCommandHandler<CreateCommand, Teacher>
	{
		private readonly TeacherCommandRepository _repository;

		public CommandHandler(
			ICommandHandler<CreateCommand, Teacher> handler,
			TeacherCommandRepository repository) : base(handler)
		{
			_repository = repository;
		}

		public override async Task<OperationResult<Teacher>> Handle(CreateCommand command)
		{
			var result = await base.Handle(command);
			if (result.IsFailed)
				return result;

			if (!command._validator.IsValid) return new OperationResult<Teacher>(command._validator.Error);
			Result<Teacher> create = await _repository.Create(command._teacher, command._findDublicate, command._getDepartment);
			return create.IsFailure ?
				new OperationResult<Teacher>(create.Error) :
				new OperationResult<Teacher>(create.Value);
		}
	}
}
