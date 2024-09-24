using StudentPerfomance.Application.EntitySchemas.Schemas.StudentGroups;
using StudentPerfomance.Application.EntitySchemas.Validators.StudentsGroupSchemaValidation;
using StudentPerfomance.Application.EntitySchemas.Validators.StudentSchemaValidation;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;
using StudentPerfomance.Application.EntitySchemas.Schemas.EducationPlans;
using StudentPerfomance.Application.EntitySchemas.Validators.EducationPlansValidations;

namespace StudentPerfomance.Application.Commands.Group.CreateStudentGroup;

internal sealed class CreateStudentGroupCommand : ICommand
{
	public StudentsGroupSchema Group { get; init; }
	public EducationPlanSchema EducationPlan { get; init; }
	public ISchemaValidator GroupValidator { get; init; }
	public ISchemaValidator EducationPlanValidator { get; init; }
	public IRepositoryExpression<StudentGroup> CheckForDublicate { get; init; }
	public IRepositoryExpression<EducationPlan> CheckForExistance { get; init; }
	public CreateStudentGroupCommand
	(
		StudentsGroupSchema group,
		EducationPlanSchema educationPlan,
		IRepositoryExpression<StudentGroup> dublicate,
		IRepositoryExpression<EducationPlan> existance
	)
	{
		Group = group;
		EducationPlan = educationPlan;
		GroupValidator = new StudentsGroupValidator(group);
		EducationPlanValidator = new EducationPlanValidator().WithYearValidation(educationPlan).WithDirectionValidation(educationPlan);
		CheckForDublicate = dublicate;
		CheckForExistance = existance;
		GroupValidator.ProcessValidation();
		EducationPlanValidator.ProcessValidation();
	}
}
