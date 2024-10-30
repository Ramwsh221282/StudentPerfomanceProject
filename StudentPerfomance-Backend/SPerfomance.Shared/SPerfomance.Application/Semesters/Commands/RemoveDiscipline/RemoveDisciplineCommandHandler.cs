using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.SemesterPlans;
using SPerfomance.Domain.Models.SemesterPlans.Abstractions;
using SPerfomance.Domain.Models.SemesterPlans.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.Semesters.Commands.RemoveDiscipline;

public class RemoveDisciplineCommandHandler
(
	ISemesterPlansRepository repository
)
 : ICommandHandler<RemoveDisciplineCommand, SemesterPlan>
{
	private readonly ISemesterPlansRepository _repository = repository;

	public async Task<Result<SemesterPlan>> Handle(RemoveDisciplineCommand command)
	{
		if (command.Discipline == null)
			return Result<SemesterPlan>.Failure(SemesterPlanErrors.NotFound());

		Result<SemesterPlan> result = command.Discipline.Semester.RemoveDiscipline(command.Discipline);

		if (!result.IsFailure)
			await _repository.Remove(command.Discipline);

		return result;
	}
}
