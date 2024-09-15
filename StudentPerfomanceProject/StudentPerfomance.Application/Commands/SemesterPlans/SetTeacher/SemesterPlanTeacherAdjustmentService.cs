using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Commands.SemesterPlans.SetTeacher;

public sealed class SemesterPlanTeacherAdjustmentService
(
	IRepositoryExpression<Teacher> teacherExistance,
	IRepositoryExpression<SemesterPlan> planExistance,
	IRepository<SemesterPlan> plans,
	IRepository<Teacher> teachers
)
: IService<SemesterPlan>
{
	private readonly SetTeacherCommand _command = new SetTeacherCommand(teacherExistance, planExistance);
	private readonly SetTeacherCommandHandler _handler = new SetTeacherCommandHandler(plans, teachers);
	public async Task<OperationResult<SemesterPlan>> DoOperation() => await _handler.Handle(_command);
}
