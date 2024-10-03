using SPerfomance.Application.Shared.Module.Schemas.Students;
using SPerfomance.Application.Shared.Module.Schemas.Students.Validators;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Commands;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.EntitySchemas;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups.Errors;
using SPerfomance.Domain.Module.Shared.Entities.Students;
using SPerfomance.Domain.Module.Shared.Entities.Students.Errors;

namespace SPerfomance.Application.Students.Module.Commands.Update;

public sealed class StudentUpdateCommand : ICommand
{
	private readonly StudentSchema _newSchema;
	private readonly IRepositoryExpression<Student> _getStudent;
	private readonly IRepositoryExpression<Student> _findDublicate;
	private readonly IRepositoryExpression<StudentGroup> _getGroup;
	private readonly ISchemaValidator _validator;
	public readonly ICommandHandler<StudentUpdateCommand, Student> Handler;
	public StudentUpdateCommand
	(
		StudentSchema newSchema,
		IRepositoryExpression<Student> getStudent,
		IRepositoryExpression<Student> findDublicate,
		IRepositoryExpression<StudentGroup> getGroup,
		IRepository<Student> students,
		IRepository<StudentGroup> groups
	)
	{
		_newSchema = newSchema;
		_getStudent = getStudent;
		_findDublicate = findDublicate;
		_getGroup = getGroup;
		_validator = new StudentValidator()
		.WithNameValidation(newSchema)
		.WithStateValidation(newSchema)
		.WithRecordbookValidation(newSchema);
		_validator.ProcessValidation();
		Handler = new StudentUpdateDefaultHandler(students);
		Handler = new StudentUpdateRecordbookHandler(Handler, students);
		Handler = new StudentUpdateStateHandler(Handler);
		Handler = new StudentUpdateGroupHandler(Handler, groups);
		Handler = new StudentUpdateNameHandler(Handler);
	}

	internal abstract class StudentUpdateCommandHandler
	(
		ICommandHandler<StudentUpdateCommand, Student> handler
	) : ICommandHandler<StudentUpdateCommand, Student>
	{
		private readonly ICommandHandler<StudentUpdateCommand, Student> _handler = handler;
		public virtual async Task<OperationResult<Student>> Handle(StudentUpdateCommand command) => await _handler.Handle(command);
	}

	internal sealed class StudentUpdateDefaultHandler : ICommandHandler<StudentUpdateCommand, Student>
	{
		private readonly IRepository<Student> _repository;
		public StudentUpdateDefaultHandler(IRepository<Student> repository)
		{
			_repository = repository;
		}

		public async Task<OperationResult<Student>> Handle(StudentUpdateCommand command)
		{
			Student? student = await _repository.GetByParameter(command._getStudent);
			return student == null ? new OperationResult<Student>(new StudentNotFoundError().ToString()) : new OperationResult<Student>(student);
		}
	}

	internal sealed class StudentUpdateGroupHandler : StudentUpdateCommandHandler
	{
		private readonly IRepository<StudentGroup> _repository;
		public StudentUpdateGroupHandler
		(
			ICommandHandler<StudentUpdateCommand, Student> handler,
			IRepository<StudentGroup> repository
		)
		: base(handler)
		{
			_repository = repository;
		}

		public override async Task<OperationResult<Student>> Handle(StudentUpdateCommand command)
		{
			OperationResult<Student> result = await base.Handle(command);
			if (result.Result == null || result.IsFailed) return result;
			if (result.Result.Group.Name.Name != command._newSchema.Group.NameInfo)
			{
				StudentGroup? group = await _repository.GetByParameter(command._getGroup);
				if (group == null)
					return new OperationResult<Student>(new GroupNotFoundError().ToString());
				result.Result.ChangeGroup(group);
			}
			return result;
		}
	}

	internal sealed class StudentUpdateNameHandler : StudentUpdateCommandHandler
	{
		public StudentUpdateNameHandler(ICommandHandler<StudentUpdateCommand, Student> handler) : base(handler) { }
		public override async Task<OperationResult<Student>> Handle(StudentUpdateCommand command)
		{
			OperationResult<Student> result = await base.Handle(command);
			if (result.Result == null || result.IsFailed) return result;
			if (result.Result.Name.Name != command._newSchema.Name ||
				result.Result.Name.Surname != command._newSchema.Surname ||
				result.Result.Name.Thirdname != command._newSchema.Thirdname)
				result.Result.ChangeName(command._newSchema.CreateName());
			return result;
		}
	}

	internal sealed class StudentUpdateRecordbookHandler : StudentUpdateCommandHandler
	{
		private readonly IRepository<Student> _repository;
		public StudentUpdateRecordbookHandler(ICommandHandler<StudentUpdateCommand, Student> handler, IRepository<Student> repository)
		: base(handler)
		{
			_repository = repository;
		}

		public override async Task<OperationResult<Student>> Handle(StudentUpdateCommand command)
		{
			OperationResult<Student> result = await base.Handle(command);
			if (result.Result == null || result.IsFailed) return result;
			if (result.Result.Recordbook.Recordbook != command._newSchema.Recordbook)
			{
				if (await _repository.HasEqualRecord(command._findDublicate))
					return new OperationResult<Student>(new StudentDublicateRecordBookError(command._newSchema.Recordbook).ToString());
				result.Result.ChangeRecordBook(command._newSchema.CreateRecordBook());
			}
			return result;
		}
	}

	internal sealed class StudentUpdateStateHandler : StudentUpdateCommandHandler
	{
		public StudentUpdateStateHandler(ICommandHandler<StudentUpdateCommand, Student> handler) : base(handler) { }
		public override async Task<OperationResult<Student>> Handle(StudentUpdateCommand command)
		{
			OperationResult<Student> result = await base.Handle(command);
			if (result.Result == null || result.IsFailed) return result;
			if (result.Result.State.State != command._newSchema.State)
				result.Result.ChangeState(command._newSchema.CreateState());
			return result;
		}
	}
}
