using StudentPerfomance.Application.EntitySchemas.Schemas.EducationPlans;
using StudentPerfomance.Application.EntitySchemas.Validators.EducationPlansValidations;
using StudentPerfomance.Application.EntitySchemas.Validators.StudentSchemaValidation;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Commands.EducationPlans.Create;

internal sealed class CreateEducationPlanCommand : ICommand
{
	public EducationPlanSchema Plan { get; init; }
	public IRepositoryExpression<EducationPlan> FindDublicate { get; init; }
	public IRepositoryExpression<EducationDirection> FindDirection { get; init; }
	public ISchemaValidator Validator { get; init; }

	public CreateEducationPlanCommand(EducationPlanSchema plan, IRepositoryExpression<EducationPlan> findDublicate, IRepositoryExpression<EducationDirection> findDirection)
	{
		Plan = plan;
		FindDublicate = findDublicate;
		FindDirection = findDirection;
		Validator = new EducationPlanValidator()
		.WithYearValidation(plan)
		.WithDirectionValidation(plan);
		Validator.ProcessValidation();
	}
}
