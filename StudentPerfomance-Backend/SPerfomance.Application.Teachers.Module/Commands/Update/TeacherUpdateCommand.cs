using SPerfomance.Application.Shared.Module.Schemas.Teachers;
using SPerfomance.Application.Shared.Module.Schemas.Teachers.Validators;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Commands;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.EntitySchemas;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.Teachers;
using SPerfomance.Domain.Module.Shared.Entities.Teachers.Errors;

namespace SPerfomance.Application.Teachers.Module.Commands.Update;

public sealed class TeacherUpdateCommand : ICommand
{
	private readonly IRepositoryExpression<Teacher> _findInitial;
	private readonly IRepositoryExpression<Teacher> _findDublicate;
	private readonly TeacherSchema _newSchema;
	private readonly ISchemaValidator _validator;
	public readonly ICommandHandler<TeacherUpdateCommand, Teacher> Handler;
	public TeacherUpdateCommand
	(
		IRepositoryExpression<Teacher> findInitial,
		IRepositoryExpression<Teacher> findDublicate,
		TeacherSchema newSchema,
		IRepository<Teacher> repository
	)
	{
		_findInitial = findInitial;
		_findDublicate = findDublicate;
		_newSchema = newSchema;
		_validator = new TeacherValidator()
		.WithNameValidation(newSchema)
		.WithConditionValidation(newSchema)
		.WithJobTitle(newSchema);
		_validator.ProcessValidation();
		Handler = new TeacherUpdateDefaultHandler(repository);
		Handler = new TeacherUpdateConditionHandler(Handler);
		Handler = new TeacherUpdateJobTitleHandler(Handler);
		Handler = new TeacherUpdateNameHandler(Handler);
	}

	internal class TeacherUpdateDecorator(ICommandHandler<TeacherUpdateCommand, Teacher> handler) : ICommandHandler<TeacherUpdateCommand, Teacher>
	{
		private readonly ICommandHandler<TeacherUpdateCommand, Teacher> _handler = handler;
		public virtual async Task<OperationResult<Teacher>> Handle(TeacherUpdateCommand command) => await _handler.Handle(command);
	}

	internal sealed class TeacherUpdateDefaultHandler(IRepository<Teacher> repository) : ICommandHandler<TeacherUpdateCommand, Teacher>
	{
		private readonly IRepository<Teacher> _repository = repository;
		public async Task<OperationResult<Teacher>> Handle(TeacherUpdateCommand command)
		{
			if (!command._validator.IsValid)
				return new OperationResult<Teacher>(command._validator.Error);
			if (await _repository.HasEqualRecord(command._findDublicate))
				return new OperationResult<Teacher>(new TeacherDublicateError
				(
					command._newSchema.Name,
					command._newSchema.Surname,
					command._newSchema.Thirdname,
					command._newSchema.Job,
					command._newSchema.Condition
				).ToString());
			Teacher? teacher = await _repository.GetByParameter(command._findInitial);
			if (teacher == null) return new OperationResult<Teacher>(new TeacherNotFoundError().ToString());
			return new OperationResult<Teacher>(teacher);
		}
	}

	internal sealed class TeacherUpdateConditionHandler : TeacherUpdateDecorator
	{
		public TeacherUpdateConditionHandler(ICommandHandler<TeacherUpdateCommand, Teacher> handler) : base(handler) { }

		public override async Task<OperationResult<Teacher>> Handle(TeacherUpdateCommand command)
		{
			OperationResult<Teacher> result = await base.Handle(command);
			if (result.Result == null || result.IsFailed)
				return result;
			if (result.Result.Condition.Value != command._newSchema.Condition)
				result.Result.ChangeCondition(command._newSchema.CreateWorkingCondition());
			return result;
		}
	}

	internal sealed class TeacherUpdateJobTitleHandler : TeacherUpdateDecorator
	{
		public TeacherUpdateJobTitleHandler(ICommandHandler<TeacherUpdateCommand, Teacher> handler) : base(handler) { }

		public override async Task<OperationResult<Teacher>> Handle(TeacherUpdateCommand command)
		{
			OperationResult<Teacher> result = await base.Handle(command);
			if (result.Result == null || result.IsFailed) return new OperationResult<Teacher>(result.Error);
			if (result.Result.JobTitle.Value != command._newSchema.Job)
				result.Result.ChangeJobTitle(command._newSchema.CreateJobTitle());
			return result;
		}
	}

	internal sealed class TeacherUpdateNameHandler : TeacherUpdateDecorator
	{
		public TeacherUpdateNameHandler(ICommandHandler<TeacherUpdateCommand, Teacher> handler) : base(handler)
		{ }

		public override async Task<OperationResult<Teacher>> Handle(TeacherUpdateCommand command)
		{
			OperationResult<Teacher> result = await base.Handle(command);
			if (result.Result == null || result.IsFailed) return result;
			if
			(
				result.Result.Name.Name != command._newSchema.Name ||
				 result.Result.Name.Surname != command._newSchema.Surname ||
				result.Result.Name.Thirdname != command._newSchema.Thirdname
			)
				result.Result.ChangeName(command._newSchema.CreateName());
			return result;
		}
	}

}
