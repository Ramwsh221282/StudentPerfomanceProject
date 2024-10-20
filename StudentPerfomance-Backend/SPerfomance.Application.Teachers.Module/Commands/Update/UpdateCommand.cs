using CSharpFunctionalExtensions;

using SPerfomance.Application.Shared.Module.CQRS.Commands;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas;
using SPerfomance.Application.Shared.Module.Schemas.Teachers;
using SPerfomance.Application.Shared.Module.Schemas.Teachers.Validators;
using SPerfomance.Application.Shared.Users.Module.Commands.Common;
using SPerfomance.Application.Teachers.Module.Repository;
using SPerfomance.Application.Teachers.Module.Repository.Expressions;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.Teachers;
using SPerfomance.Domain.Module.Shared.Entities.Teachers.Errors;
using SPerfomance.Domain.Module.Shared.Entities.Users;

namespace SPerfomance.Application.Teachers.Module.Commands.Update;

internal sealed class UpdateCommand : ICommand
{
	private readonly TeacherSchema _newSchema;
	private readonly IRepositoryExpression<Teacher> _getInitial;
	private readonly IRepositoryExpression<Teacher> _findDublicate;
	private readonly TeacherQueryRepository _repository;
	private readonly ISchemaValidator _validator;

	public readonly ICommandHandler<UpdateCommand, Teacher> Handler;

	public UpdateCommand(TeacherSchema oldSchema, TeacherSchema newSchema, string token)
	{
		_newSchema = newSchema;
		_getInitial = ExpressionsFactory.GetTeacher(oldSchema.ToRepositoryObject());
		_findDublicate = ExpressionsFactory.GetTeacher(newSchema.ToRepositoryObject());
		_repository = new TeacherQueryRepository();
		_validator = new TeacherValidator().WithNameValidation(newSchema).WithJobTitle(newSchema).WithConditionValidation(newSchema);
		_validator.ProcessValidation();
		Handler = new VerificationHandler<UpdateCommand, Teacher>(token, User.Admin);
		Handler = new DefaultHandler(Handler, _repository);
		Handler = new UpdateNameHandler(Handler);
		Handler = new UpdateWorkingCondition(Handler);
		Handler = new UpdateJobTitleHandler(Handler);
		Handler = new CommitHandler(Handler, _repository);
	}

	internal sealed class DefaultHandler : DecoratedCommandHandler<UpdateCommand, Teacher>
	{
		private readonly TeacherQueryRepository _repository;

		public DefaultHandler(
			ICommandHandler<UpdateCommand, Teacher> handler,
			TeacherQueryRepository repository) : base(handler)
		{
			_repository = repository;
		}

		public override async Task<OperationResult<Teacher>> Handle(UpdateCommand command)
		{
			var result = await base.Handle(command);
			if (result.IsFailed)
				return result;

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

	internal sealed class UpdateNameHandler : DecoratedCommandHandler<UpdateCommand, Teacher>
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

	internal sealed class UpdateJobTitleHandler : DecoratedCommandHandler<UpdateCommand, Teacher>
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

	internal sealed class UpdateWorkingCondition : DecoratedCommandHandler<UpdateCommand, Teacher>
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

	internal sealed class CommitHandler : DecoratedCommandHandler<UpdateCommand, Teacher>
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
