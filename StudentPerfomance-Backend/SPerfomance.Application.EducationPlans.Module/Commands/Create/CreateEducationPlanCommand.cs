using SPerfomance.Application.Shared.Module.Schemas.EducationPlans;
using SPerfomance.Application.Shared.Module.Schemas.EducationPlans.Validators;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Commands;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.EntitySchemas;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans;

namespace SPerfomance.Application.EducationPlans.Module.Commands.Create;

internal sealed class CreateEducationPlanCommand : ICommand
{
	public EducationPlanSchema Plan { get; init; }
	public IRepositoryExpression<EducationPlan> FindPlanDublicate { get; init; }
	public IRepositoryExpression<EducationDirection> FindDirection { get; init; }
	public ISchemaValidator PlanValidator { get; init; }

	public CreateEducationPlanCommand
	(
		EducationPlanSchema plan,
		IRepositoryExpression<EducationPlan> findPlanDublicate,
		IRepositoryExpression<EducationDirection> findDirection
	)
	{
		Plan = plan;
		FindPlanDublicate = findPlanDublicate;
		FindDirection = findDirection;
		PlanValidator = new EducationPlanValidator()
		.WithYearValidation(plan);
		PlanValidator.ProcessValidation();
	}
}
