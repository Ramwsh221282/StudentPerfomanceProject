using CSharpFunctionalExtensions;

using SPerfomance.Application.Shared.Module.CQRS.Commands;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas;
using SPerfomance.Application.Shared.Module.Schemas.Teachers;
using SPerfomance.Application.Shared.Module.Schemas.Teachers.Validators;
using SPerfomance.Application.Teachers.Module.Repository;
using SPerfomance.Application.Teachers.Module.Repository.Expressions;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.Teachers;
using SPerfomance.Domain.Module.Shared.Entities.Teachers.Errors;

namespace SPerfomance.Application.Teachers.Module.Commands.Update;

internal sealed class UpdateCommand : ICommand
{
	private readonly TeacherSchema _newSchema;
	private readonly IRepositoryExpression<Teacher> _getInitial;
	private readonly IRepositoryExpression<Teacher> _findDublicate;
	private readonly TeacherQueryRepository _repository;
	private readonly ISchemaValidator _validator;
	public readonly ICommandHandler<UpdateCommand, Teacher> Handler;
	public UpdateCommand(TeacherSchema oldSchema, TeacherSchema newSchema)
	{
		_newSchema = newSchema;
		_getInitial = ExpressionsFactory.GetTeacher(oldSchema.ToRepositoryObject());
		_findDublicate = ExpressionsFactory.GetTeacher(newSchema.ToRepositoryObject());
		_repository = new TeacherQueryRepository();
		_validator = new TeacherValidator().WithNameValidation(newSchema).WithJobTitle(newSchema).WithConditionValidation(newSchema);
		_validator.ProcessValidation();
		Handler = new DefaultHandler(_repository);
		Handler = new UpdateNameHandler(Handler);
		Handler = new UpdateWorkingCondition(Handler);
		Handler = new UpdateJobTitleHandler(Handler);
		Handler = new CommitHandler(Handler, _repository);
	}

	internal sealed class DefaultHandler(TeacherQueryRepository repository) : ICommandHandler<UpdateCommand, Teacher>
	{
		private readonly TeacherQueryRepository _repository = repository;
		public async Task<OperationResult<Teacher>> Handle(UpdateCommand command)
		{
			if (await _repository.HasEqualRecord(command._findDublicate))
				return new OperationResult<Teacher>(new TeacherDublicateError
				(
					command._newSchema.Name,
					command._newSchema.Surname,
					command._newSchema.Thirdname,
					command._newSchema.JobTitle,
					command._newSchema.WorkingCondition
				).ToString());
			Teacher? teacher = await _repository.GetByParameter(command._getInitial);
			return teacher == null ?
				new OperationResult<Teacher>(new TeacherNotFoundError().ToString()) :
				new OperationResult<Teacher>(teacher);
		}
	}

	internal abstract class HandlerDecorator(ICommandHandler<UpdateCommand, Teacher> handler) : ICommandHandler<UpdateCommand, Teacher>
	{
		private readonly ICommandHandler<UpdateCommand, Teacher> _handler = handler;
		public virtual async Task<OperationResult<Teacher>> Handle(UpdateCommand command) => await _handler.Handle(command);
	}

	internal sealed class UpdateNameHandler : HandlerDecorator
	{
		public UpdateNameHandler(ICommandHandler<UpdateCommand, Teacher> handler) : base(handler) { }

		public override async Task<OperationResult<Teacher>> Handle(UpdateCommand command)
		{
			OperationResult<Teacher> result = await base.Handle(command);
			if (result.Result == null || result.IsFailed) return result;
			Result update = result.Result.ChangeName(command._newSchema.CreateName());
			return update.IsFailure ? new OperationResult<Teacher>(update.Error) : result;
		}
	}

	internal sealed class UpdateJobTitleHandler : HandlerDecorator
	{
		public UpdateJobTitleHandler(ICommandHandler<UpdateCommand, Teacher> handler) : base(handler) { }
		public override async Task<OperationResult<Teacher>> Handle(UpdateCommand command)
		{
			OperationResult<Teacher> result = await base.Handle(command);
			if (result.Result == null || result.IsFailed) return result;
			Result update = result.Result.ChangeJobTitle(command._newSchema.CreateJobTitle());
			return update.IsFailure ? new OperationResult<Teacher>(update.Error) : result;
		}
	}

	internal sealed class UpdateWorkingCondition : HandlerDecorator
	{
		public UpdateWorkingCondition(ICommandHandler<UpdateCommand, Teacher> handler) : base(handler) { }
		public override async Task<OperationResult<Teacher>> Handle(UpdateCommand command)
		{
			OperationResult<Teacher> result = await base.Handle(command);
			if (result.Result == null || result.IsFailed) return result;
			Result update = result.Result.ChangeCondition(command._newSchema.CreateWorkingCondition());
			return update.IsFailure ? new OperationResult<Teacher>(update.Error) : result;
		}
	}

	internal sealed class CommitHandler : HandlerDecorator
	{
		private readonly TeacherQueryRepository _repository;
		public CommitHandler(ICommandHandler<UpdateCommand, Teacher> handler, TeacherQueryRepository repository) : base(handler)
		{
			_repository = repository;
		}

		public override async Task<OperationResult<Teacher>> Handle(UpdateCommand command)
		{
			OperationResult<Teacher> result = await base.Handle(command);
			if (result.Result == null || result.IsFailed) return result;
			await _repository.Commit();
			return new OperationResult<Teacher>(result.Result);
		}
	}
}
