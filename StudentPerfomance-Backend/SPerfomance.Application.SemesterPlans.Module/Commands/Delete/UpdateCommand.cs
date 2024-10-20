using CSharpFunctionalExtensions;

using SPerfomance.Application.SemesterPlans.Module.Repository;
using SPerfomance.Application.SemesterPlans.Module.Repository.Expressions;
using SPerfomance.Application.Shared.Module.CQRS.Commands;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas;
using SPerfomance.Application.Shared.Module.Schemas.SemesterPlans;
using SPerfomance.Application.Shared.Module.Schemas.SemesterPlans.Validators;
using SPerfomance.Application.Shared.Module.Schemas.Semesters;
using SPerfomance.Application.Shared.Users.Module.Commands.Common;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.SemesterPlans;
using SPerfomance.Domain.Module.Shared.Entities.SemesterPlans.Errors;
using SPerfomance.Domain.Module.Shared.Entities.Semesters;
using SPerfomance.Domain.Module.Shared.Entities.Semesters.Errors;
using SPerfomance.Domain.Module.Shared.Entities.Users;

namespace SPerfomance.Application.SemesterPlans.Module.Commands.Delete;

internal sealed class UpdateCommand : ICommand
{
	private readonly SemesterPlanSchema _initialSchema;
	private readonly SemesterPlanSchema _newSchema;
	private readonly IRepositoryExpression<Semester> _getSemester;
	private readonly SemesterPlansQueryRepository _repository;
	private readonly ISchemaValidator _validator;

	public readonly ICommandHandler<UpdateCommand, SemesterPlan> Handler;

	public UpdateCommand(SemesterPlanSchema oldSchema, SemesterPlanSchema newSchema, string token)
	{
		_initialSchema = oldSchema;
		_newSchema = newSchema;
		_repository = new SemesterPlansQueryRepository();
		_getSemester = ExpressionsFactory.GetSemester(oldSchema.Semester.ToRepositoryObject());
		_validator = new SemesterPlanValidator().WithDisciplineValidation(_newSchema);
		_validator.ProcessValidation();
		Handler = new VerificationHandler<UpdateCommand, SemesterPlan>(token, User.Admin);
		Handler = new CommandHandler(Handler, _repository);
	}

	internal sealed class CommandHandler : DecoratedCommandHandler<UpdateCommand, SemesterPlan>
	{
		private readonly SemesterPlansQueryRepository _repository;

		public CommandHandler(
			ICommandHandler<UpdateCommand, SemesterPlan> handler,
			SemesterPlansQueryRepository repository
			) : base(handler)
		{
			_repository = repository;
		}

		public override async Task<OperationResult<SemesterPlan>> Handle(UpdateCommand command)
		{
			OperationResult<SemesterPlan> result = await base.Handle(command);
			if (result.IsFailed)
				return result;

			if (!command._validator.IsValid)
				return new OperationResult<SemesterPlan>(command._validator.Error);

			Semester? semester = await _repository.GetByParameter(command._getSemester);
			if (semester == null)
				return new OperationResult<SemesterPlan>(new SemesterNotFoundError().ToString());

			Result<SemesterPlan> initial = semester.GetContract(command._initialSchema.Discipline);
			if (initial.IsFailure)
				return new OperationResult<SemesterPlan>(initial.Error);

			Result<SemesterPlan> dublicate = semester.GetContract(command._newSchema.Discipline);
			if (dublicate.IsSuccess)
				return new OperationResult<SemesterPlan>(new SemesterPlanDublicateError(
					semester.Number.Value,
					semester.Plan.Year.Year,
					semester.Plan.Direction.Name.Name,
					command._newSchema.Discipline
				).ToString());

			initial.Value.Discipline.ChangeName(command._newSchema.Discipline);
			await _repository.Commit();
			return new OperationResult<SemesterPlan>(initial.Value);
		}
	}
}
