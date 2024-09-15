using StudentPerfomance.Application.EntitySchemas.Schemas.Discplines;
using StudentPerfomance.Application.EntitySchemas.Schemas.Semesters;
using StudentPerfomance.Application.EntitySchemas.Validators.Disciplines;
using StudentPerfomance.Application.EntitySchemas.Validators.StudentSchemaValidation;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Commands.SemesterPlans.CreateSemesterPlan;

internal sealed class CreateSemesterPlanCommand : ICommand
{
	public DisciplineSchema Discipline { get; init; }
	public SemesterSchema Semester { get; init; }
	public IRepositoryExpression<SemesterPlan> DublicatePlan { get; init; }
	public IRepositoryExpression<Discipline> DublicateDiscipline { get; init; }
	public IRepositoryExpression<Semester> SemesterExistance { get; init; }
	public ISchemaValidator Validator { get; init; }
	public CreateSemesterPlanCommand
	(
		DisciplineSchema discipline,
		SemesterSchema semester,
		IRepositoryExpression<SemesterPlan> dublicatePlan,
		IRepositoryExpression<Discipline> dublicateDiscipline,
		IRepositoryExpression<Semester> semesterExistance
	)
	{
		Discipline = discipline;
		Semester = semester;
		DublicatePlan = dublicatePlan;
		DublicateDiscipline = dublicateDiscipline;
		SemesterExistance = semesterExistance;
		Validator = new DisciplineValidator(discipline);
		Validator.ProcessValidation();
	}
}
