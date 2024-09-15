using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Commands.SemesterPlans.SetTeacher;

internal sealed class SetTeacherCommand
(
	IRepositoryExpression<Teacher> teacherExistance,
	IRepositoryExpression<SemesterPlan> planExistance
)
: ICommand
{
	public IRepositoryExpression<Teacher> TeacherExistance { get; init; } = teacherExistance;
	public IRepositoryExpression<SemesterPlan> PlanExistance { get; init; } = planExistance;
}
