using StudentPerfomance.Application.EntitySchemas.Schemas.Discplines;
using StudentPerfomance.Application.EntitySchemas.Schemas.Semesters;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Commands.SemesterPlans.CreateSemesterPlan;

public sealed class SemesterPlanCreationService
(
	DisciplineSchema discipline,
	SemesterSchema semester,
	IRepositoryExpression<SemesterPlan> dublicatePlan,
	IRepositoryExpression<Discipline> dublicateDiscipline,
	IRepositoryExpression<Semester> semesterExistance,
	IRepository<SemesterPlan> plans,
	IRepository<Semester> semesters,
	IRepository<Discipline> disciplines
)
: IService<SemesterPlan>
{
	private readonly CreateSemesterPlanCommand _command = new CreateSemesterPlanCommand
	(
		discipline,
		semester,
		dublicatePlan,
		dublicateDiscipline,
		semesterExistance
	);
	private readonly CreateSemesterPlanCommandHandler _handler = new CreateSemesterPlanCommandHandler(plans, semesters, disciplines);

	public async Task<OperationResult<SemesterPlan>> DoOperation() => await _handler.Handle(_command);
}
