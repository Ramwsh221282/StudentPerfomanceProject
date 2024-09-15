using StudentPerfomance.Application.EntitySchemas.Validators;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Commands.SemesterPlans.SetTeacher;

internal sealed class SetTeacherCommandHandler
(
	IRepository<SemesterPlan> plans,
	IRepository<Teacher> teachers
)
: CommandWithErrorBuilder, ICommandHandler<SetTeacherCommand, OperationResult<SemesterPlan>>
{
	private readonly IRepository<SemesterPlan> _plans = plans;
	private readonly IRepository<Teacher> _teachers = teachers;

	public async Task<OperationResult<SemesterPlan>> Handle(SetTeacherCommand command)
	{
		SemesterPlan? plan = await _plans.GetByParameter(command.PlanExistance);
		plan.ValidateNullability("План семестра не найден", this);
		if (plan != null && plan.HasTeacher())
			AppendError("Невозможно установить более 1 преподавателя на этот план");
		Teacher? teacher = await _teachers.GetByParameter(command.TeacherExistance);
		teacher.ValidateNullability("Преподаватель не найден", this);
		return await this.ProcessAsync(async () =>
		{
			plan.LinkedDiscipline.SetTeacher(teacher);
			await _plans.Commit();
			return new OperationResult<SemesterPlan>(plan);
		});
	}
}
