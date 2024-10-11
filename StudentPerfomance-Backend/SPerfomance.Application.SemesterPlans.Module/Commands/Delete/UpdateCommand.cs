using SPerfomance.Application.SemesterPlans.Module.Repository;
using SPerfomance.Application.SemesterPlans.Module.Repository.Expressions;
using SPerfomance.Application.Shared.Module.CQRS.Commands;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas;
using SPerfomance.Application.Shared.Module.Schemas.SemesterPlans;
using SPerfomance.Application.Shared.Module.Schemas.SemesterPlans.Validators;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.SemesterPlans;
using SPerfomance.Domain.Module.Shared.Entities.SemesterPlans.Errors;

namespace SPerfomance.Application.SemesterPlans.Module.Commands.Delete;

internal sealed class UpdateCommand : ICommand
{
	private readonly SemesterPlanSchema _newSchema;
	private readonly SemesterPlansQueryRepository _repository;
	private readonly IRepositoryExpression<SemesterPlan> _getInitial;
	private readonly IRepositoryExpression<SemesterPlan> _findDublicate;
	private readonly ISchemaValidator _validator;
	public readonly ICommandHandler<UpdateCommand, SemesterPlan> Handler;

	public UpdateCommand(SemesterPlanSchema oldSchema, SemesterPlanSchema newSchema)
	{
		_newSchema = newSchema;
		_repository = new SemesterPlansQueryRepository();
		_getInitial = ExpressionsFactory.GetPlan(oldSchema.ToRepositoryObject());
		_findDublicate = ExpressionsFactory.GetPlan(newSchema.ToRepositoryObject());
		_validator = new SemesterPlanValidator().WithDisciplineValidation(_newSchema);
		_validator.ProcessValidation();
		Handler = new CommandHandler(_repository);
	}

	internal sealed class CommandHandler(SemesterPlansQueryRepository repository) : ICommandHandler<UpdateCommand, SemesterPlan>
	{
		private readonly SemesterPlansQueryRepository _repository = repository;
		public async Task<OperationResult<SemesterPlan>> Handle(UpdateCommand command)
		{
			if (!command._validator.IsValid)
				return new OperationResult<SemesterPlan>(command._validator.Error);

			SemesterPlan? initial = await _repository.GetByParameter(command._getInitial);
			if (initial == null)
				return new OperationResult<SemesterPlan>(new SemesterPlanNotFoundError().ToString());

			if (await _repository.HasEqualRecord(command._findDublicate))
				return new OperationResult<SemesterPlan>(new SemesterPlanDublicateError
				(
					initial.Semester.Number.Value,
					initial.Semester.Plan.Year.Year,
					initial.Semester.Plan.Direction.Name.Name,
					command._newSchema.Discipline
				)
				.ToString());

			initial.Discipline.ChangeName(command._newSchema.Discipline);
			await _repository.Commit();
			return new OperationResult<SemesterPlan>(initial);
		}
	}
}
