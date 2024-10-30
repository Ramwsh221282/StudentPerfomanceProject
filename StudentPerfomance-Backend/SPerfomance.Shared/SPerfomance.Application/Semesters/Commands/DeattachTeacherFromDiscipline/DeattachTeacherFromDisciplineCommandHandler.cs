using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.SemesterPlans;
using SPerfomance.Domain.Models.SemesterPlans.Abstractions;
using SPerfomance.Domain.Models.SemesterPlans.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.Semesters.Commands.DeattachTeacherFromDiscipline;

public class DeattachTeacherFromDisciplineCommandHandler
(
	ISemesterPlansRepository repository
)
: ICommandHandler<DeattachTeacherFromDisciplineCommand, SemesterPlan>
{
	private readonly ISemesterPlansRepository _repository = repository;

	public async Task<Result<SemesterPlan>> Handle(DeattachTeacherFromDisciplineCommand command)
	{
		if (command.Discipline == null)
			return Result<SemesterPlan>.Failure(SemesterPlanErrors.NotFound());

		Result<SemesterPlan> discipline = command.Discipline.Semester.DeattachTeacherFromDiscipline(command.Discipline);
		if (discipline.IsFailure)
			return discipline;

		await _repository.DeattachTeacherId(discipline.Value);
		return Result<SemesterPlan>.Success(discipline.Value);
	}
}
