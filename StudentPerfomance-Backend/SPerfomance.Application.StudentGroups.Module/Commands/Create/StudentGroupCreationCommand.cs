using SPerfomance.Application.Shared.Module.Schemas.EducationDirections.Validators;
using SPerfomance.Application.Shared.Module.Schemas.EducationPlans.Validators;
using SPerfomance.Application.Shared.Module.Schemas.StudentGroups;
using SPerfomance.Application.Shared.Module.Schemas.StudentGroups.Validators;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Commands;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.CompositeEntityValidator;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups;

namespace SPerfomance.Application.StudentGroups.Module.Commands.Create;

internal sealed class StudentGroupCreationCommand : ICommand
{
	public StudentsGroupSchema Group { get; init; }
	public IRepositoryExpression<StudentGroup> CheckGroupDublicate { get; init; }
	public IRepositoryExpression<EducationPlan> FindEducationPlan { get; init; }
	public CompositeValidator Validator { get; init; }
	public StudentGroupCreationCommand
	(
		StudentsGroupSchema group,
		IRepositoryExpression<StudentGroup> checkGroupDublicate,
		IRepositoryExpression<EducationPlan> findEducationPlan
	)
	{
		Group = group;
		CheckGroupDublicate = checkGroupDublicate;
		FindEducationPlan = findEducationPlan;
		Validator = new CompositeValidator
		(
			new StudentGroupSchemaValidator().WithNameValidation(group),
			new EducationPlanValidator().WithYearValidation(group.PlanInfo),
			new EducationDirectionValidator()
			.WithNameValidation(group.PlanInfo.Direction)
			.WithTypeValidation(group.PlanInfo.Direction)
			.WithCodeValidator(group.PlanInfo.Direction)
		);
		Validator.ProcessValidation();
	}
}
