using SPerfomance.Application.Shared.Module.CQRS.Commands;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas;
using SPerfomance.Application.Shared.Module.Schemas.StudentGroups.Validators;
using SPerfomance.Application.Shared.Module.Schemas.Students;
using SPerfomance.Application.Shared.Module.Schemas.Students.Validators;
using SPerfomance.Application.Students.Module.Repository;
using SPerfomance.Application.Students.Module.Repository.Expressions;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups.Errors;
using SPerfomance.Domain.Module.Shared.Entities.Students;
using SPerfomance.Domain.Module.Shared.Entities.Students.Errors;

namespace SPerfomance.Application.Students.Module.Commands.Update;

internal sealed class UpdateCommand : ICommand
{
	private readonly StudentSchema _newSchema;
	private readonly IRepositoryExpression<Student> _getInitial;
	private readonly IRepositoryExpression<Student> _findDublicate;
	private readonly IRepositoryExpression<StudentGroup> _getGroup;
	private readonly StudentQueryRepository _repository;
	private readonly ISchemaValidator _validator = new StudentGroupSchemaValidator();
	public readonly ICommandHandler<UpdateCommand, Student> Handler;
	public UpdateCommand(StudentSchema oldSchema, StudentSchema newSchema)
	{
		_newSchema = newSchema;
		_getInitial = ExpressionsFactory.GetStudent(oldSchema.ToRepositoryObject());
		_findDublicate = ExpressionsFactory.GetByRecordbook(newSchema.ToRepositoryObject());
		_getGroup = ExpressionsFactory.GetGroupByName(newSchema.ToRepositoryObject());
		_repository = new StudentQueryRepository();
		_validator = new StudentValidator()
		.WithNameValidation(newSchema)
		.WithStateValidation(newSchema)
		.WithRecordbookValidation(newSchema);
		_validator.ProcessValidation();
		Handler = new DefaultHandler(_repository);
		Handler = new UpdateRecordBookHandler(Handler, _repository);
		Handler = new UpdateGroupHandler(Handler, _repository);
		Handler = new UpdateNameHandler(Handler);
		Handler = new UpdateStateHandler(Handler);
		Handler = new CommitHandler(Handler, _repository);
	}

	internal abstract class DecoratorHandler(ICommandHandler<UpdateCommand, Student> handler) : ICommandHandler<UpdateCommand, Student>
	{
		private readonly ICommandHandler<UpdateCommand, Student> _handler = handler;
		public virtual async Task<OperationResult<Student>> Handle(UpdateCommand command) => await _handler.Handle(command);
	}

	internal sealed class DefaultHandler(StudentQueryRepository repository) : ICommandHandler<UpdateCommand, Student>
	{
		private readonly StudentQueryRepository _repository = repository;
		public async Task<OperationResult<Student>> Handle(UpdateCommand command)
		{
			if (!command._validator.IsValid) return new OperationResult<Student>(command._validator.Error);
			Student? student = await _repository.GetByParameter(command._getInitial);
			return student == null ?
				new OperationResult<Student>(new StudentNotFoundError().ToString()) :
				new OperationResult<Student>(student);
		}
	}

	internal sealed class UpdateNameHandler : DecoratorHandler
	{
		public UpdateNameHandler(ICommandHandler<UpdateCommand, Student> handler) : base(handler) { }
		public override async Task<OperationResult<Student>> Handle(UpdateCommand command)
		{
			OperationResult<Student> result = await base.Handle(command);
			if (result.Result == null || result.IsFailed) return result;
			if (result.Result.Name.Name != command._newSchema.Name ||
				result.Result.Name.Surname != command._newSchema.Surname ||
				 result.Result.Name.Thirdname != command._newSchema.Thirdname
				)
				result.Result.ChangeName(command._newSchema.CreateName());
			return result;
		}
	}

	internal sealed class UpdateStateHandler : DecoratorHandler
	{
		public UpdateStateHandler(ICommandHandler<UpdateCommand, Student> handler) : base(handler) { }
		public override async Task<OperationResult<Student>> Handle(UpdateCommand command)
		{
			OperationResult<Student> result = await base.Handle(command);
			if (result.Result == null || result.IsFailed) return result;
			if (result.Result.State.State != command._newSchema.State)
				result.Result.ChangeState(command._newSchema.CreateState());
			return result;
		}
	}

	internal sealed class UpdateRecordBookHandler : DecoratorHandler
	{
		private readonly StudentQueryRepository _repository;
		public UpdateRecordBookHandler(ICommandHandler<UpdateCommand, Student> handler, StudentQueryRepository repository) : base(handler)
		{
			_repository = repository;
		}

		public override async Task<OperationResult<Student>> Handle(UpdateCommand command)
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

	internal sealed class UpdateGroupHandler : DecoratorHandler
	{
		private readonly StudentQueryRepository _repository;

		public UpdateGroupHandler(ICommandHandler<UpdateCommand, Student> handler, StudentQueryRepository repository) : base(handler)
		{
			_repository = repository;
		}

		public override async Task<OperationResult<Student>> Handle(UpdateCommand command)
		{
			OperationResult<Student> result = await base.Handle(command);
			if (result.Result == null || result.IsFailed) return result;
			if (result.Result.Group.Name.Name != command._newSchema.Group.Name)
			{
				StudentGroup? group = await _repository.GetByParameter(command._getGroup);
				if (group == null)
					return new OperationResult<Student>(new GroupNotFoundError().ToString());
				result.Result.ChangeGroup(group);
			}
			return result;
		}
	}

	internal sealed class CommitHandler : DecoratorHandler
	{
		private readonly StudentQueryRepository _repository;
		public CommitHandler(ICommandHandler<UpdateCommand, Student> handler, StudentQueryRepository repository) : base(handler)
		{
			_repository = repository;
		}

		public override async Task<OperationResult<Student>> Handle(UpdateCommand command)
		{
			OperationResult<Student> result = await base.Handle(command);
			if (result.Result == null || result.IsFailed) return result;
			try
			{
				await _repository.Commit();
				return result;
			}
			catch { return result; }
		}
	}
}
