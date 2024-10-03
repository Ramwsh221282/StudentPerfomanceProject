using SPerfomance.Application.EducationPlans.Module.Commands.Create.Decorators.CreationPolicies;
using SPerfomance.Application.Shared.Module.Schemas.EducationPlans;
using SPerfomance.Application.Shared.Module.Schemas.EducationPlans.Validators;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Commands;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.EntitySchemas;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans.Errors;
using SPerfomance.Domain.Module.Shared.Entities.Semesters;

namespace SPerfomance.Application.EducationPlans.Module.Commands.Create;

public sealed class CreateEducationPlanCommand : ICommand
{
	private readonly EducationPlanSchema _plan;
	private readonly IRepositoryExpression<EducationPlan> _findPlanDublicate;
	private readonly IRepositoryExpression<EducationDirection> _getDirection;
	private readonly ISchemaValidator _planValidator;
	public readonly ICommandHandler<CreateEducationPlanCommand, EducationPlan> Handler;
	public CreateEducationPlanCommand
	(
		EducationPlanSchema plan,
		IRepositoryExpression<EducationPlan> findPlanDublicate,
		IRepositoryExpression<EducationDirection> findDirection,
		IRepository<EducationPlan> plans,
		IRepository<EducationDirection> directions,
		IRepository<Semester> semesters
	)
	{
		_plan = plan;
		_findPlanDublicate = findPlanDublicate;
		_getDirection = findDirection;
		_planValidator = new EducationPlanValidator()
		.WithYearValidation(plan);
		_planValidator.ProcessValidation();
		Handler = new CreateEducationPlanDefaultHandler(plans, directions);
		Handler = new CreateEducationPlanWithSemestersHandler(Handler, semesters);
	}

	internal class CreateEducationPlanDecorator
	(
		ICommandHandler<CreateEducationPlanCommand, EducationPlan> handler
	) : ICommandHandler<CreateEducationPlanCommand, EducationPlan>
	{
		private readonly ICommandHandler<CreateEducationPlanCommand, EducationPlan> _handler = handler;
		public virtual async Task<OperationResult<EducationPlan>> Handle(CreateEducationPlanCommand command) => await _handler.Handle(command);
	}

	internal sealed class CreateEducationPlanDefaultHandler
	(
		IRepository<EducationPlan> plans,
		IRepository<EducationDirection> directions
	) : ICommandHandler<CreateEducationPlanCommand, EducationPlan>
	{
		private readonly IRepository<EducationPlan> _plans = plans;
		private readonly IRepository<EducationDirection> _directions = directions;
		public async Task<OperationResult<EducationPlan>> Handle(CreateEducationPlanCommand command)
		{
			if (await _plans.HasEqualRecord(command._findPlanDublicate)) return new OperationResult<EducationPlan>(new EducationPlanDublicateError().ToString());
			if (!command._planValidator.IsValid) return new OperationResult<EducationPlan>(command._planValidator.Error);
			EducationDirection? direction = await _directions.GetByParameter(command._getDirection);
			if (direction == null) return new OperationResult<EducationPlan>(new EducationPlanWithoutDirectionError().ToString());
			EducationPlan plan = command._plan.CreateDomainObject(direction);
			return new OperationResult<EducationPlan>(plan);
		}
	}

	internal sealed class CreateEducationPlanWithSemestersHandler
	(
		ICommandHandler<CreateEducationPlanCommand, EducationPlan> handler, IRepository<Semester> semesters
	) : CreateEducationPlanDecorator(handler)
	{
		private readonly IRepository<Semester> _semesters = semesters;
		public override async Task<OperationResult<EducationPlan>> Handle(CreateEducationPlanCommand command)
		{
			OperationResult<EducationPlan> result = await base.Handle(command);
			if (result.Result == null || result.IsFailed) return new OperationResult<EducationPlan>(result.Error);
			ICreateEducationPlanPolicy policy = new CreateEducationPlanPolicy(result.Result, _semesters);
			await policy.ExecutePolicy();
			return result;
		}
	}
}
