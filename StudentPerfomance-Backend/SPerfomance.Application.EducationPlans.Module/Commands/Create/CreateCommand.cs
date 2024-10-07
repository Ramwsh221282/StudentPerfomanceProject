using CSharpFunctionalExtensions;

using SPerfomance.Application.EducationPlans.Module.Repository;
using SPerfomance.Application.EducationPlans.Module.Repository.Expressions;
using SPerfomance.Application.Shared.Module.CQRS.Commands;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas;
using SPerfomance.Application.Shared.Module.Schemas.EducationPlans;
using SPerfomance.Application.Shared.Module.Schemas.EducationPlans.Validators;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans;

namespace SPerfomance.Application.EducationPlans.Module.Commands.Create;

internal sealed class CreateCommand : ICommand
{
	private readonly EducationPlanSchema _plan;
	private readonly IRepositoryExpression<EducationPlan> _findDublicate;
	private readonly IRepositoryExpression<EducationDirection> _findDirection;
	private readonly ISchemaValidator _validator;
	private readonly EducationPlanCommandRepository _repository;
	public readonly ICommandHandler<CreateCommand, EducationPlan> Handler;
	public CreateCommand(EducationPlanSchema plan)
	{
		_plan = plan;
		_findDublicate = ExpressionsFactory.GetPlan(plan.ToRepositoryObject());
		_findDirection = ExpressionsFactory.GetDirection(plan.ToRepositoryObject());
		_validator = new EducationPlanValidator().WithYearValidation(_plan);
		_validator.ProcessValidation();
		_repository = new EducationPlanCommandRepository();
		Handler = new CommandHandler(_repository);
	}

	internal sealed class CommandHandler(EducationPlanCommandRepository repository) : ICommandHandler<CreateCommand, EducationPlan>
	{
		private readonly EducationPlanCommandRepository _repository = repository;
		public async Task<OperationResult<EducationPlan>> Handle(CreateCommand command)
		{
			if (!command._validator.IsValid) return new OperationResult<EducationPlan>(command._validator.Error);
			Result<EducationPlan> create = await _repository.Create(command._plan, command._findDirection, command._findDublicate);
			return create.IsFailure ?
				new OperationResult<EducationPlan>(create.Error) :
				new OperationResult<EducationPlan>(create.Value);
		}
	}
}
