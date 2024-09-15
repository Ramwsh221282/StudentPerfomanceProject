using StudentPerfomance.Application.EntitySchemas.Validators;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Commands.SemesterPlans.CreateSemesterPlan;

internal sealed class CreateSemesterPlanCommandHandler
(
	IRepository<SemesterPlan> plans,
	IRepository<Semester> semesters,
	IRepository<Discipline> disciplines
)
: CommandWithErrorBuilder, ICommandHandler<CreateSemesterPlanCommand, OperationResult<SemesterPlan>>
{
	private readonly IRepository<SemesterPlan> _plans = plans;
	private readonly IRepository<Semester> _semester = semesters;
	private readonly IRepository<Discipline> _disciplines = disciplines;
	public async Task<OperationResult<SemesterPlan>> Handle(CreateSemesterPlanCommand command)
	{
		command.Validator.ValidateSchema(this);
		Semester? semester = await _semester.GetByParameter(command.SemesterExistance);
		semester.ValidateNullability("Семестр не найден", this);
		await _plans.ValidateExistance(command.DublicatePlan, "План семестра с таким номером и дисциплиной уже существует", this);
		Discipline? discipline = await _disciplines.GetByParameter(command.DublicateDiscipline);
		return (discipline == null) switch
		{
			true => await this.ProcessAsync(async () =>
			{
				Discipline discipline = Discipline.Create(Guid.NewGuid(), command.Discipline.Name).Value;
				await _disciplines.Create(discipline);
				SemesterPlan plan = SemesterPlan.Create(Guid.NewGuid(), semester, discipline).Value;
				await _plans.Create(plan);
				return new OperationResult<SemesterPlan>(plan);
			}),
			false => await this.ProcessAsync(async () =>
			{
				SemesterPlan plan = SemesterPlan.Create(Guid.NewGuid(), semester, discipline).Value;
				await _plans.Create(plan);
				return new OperationResult<SemesterPlan>(plan);
			})
		};
	}
}
