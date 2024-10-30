using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.EducationPlans;
using SPerfomance.Domain.Models.EducationPlans.Abstractions;
using SPerfomance.Domain.Models.EducationPlans.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.EducationPlans.Commands.ChangeEducationPlanYear;

public class ChangeEducationPlanYearCommandHandler
(
	IEducationPlansRepository repository
)
 : ICommandHandler<ChangeEducationPlanYearCommand, EducationPlan>
{
	private readonly IEducationPlansRepository _repository = repository;

	public async Task<Result<EducationPlan>> Handle(ChangeEducationPlanYearCommand command)
	{
		if (command.Plan == null)
			return Result<EducationPlan>.Failure(EducationPlanErrors.NotFoundError());

		Result<EducationPlan> updated = command.Plan.ChangeYear(command.NewYear);
		if (updated.IsFailure)
			return updated;

		await _repository.Update(updated.Value);
		return Result<EducationPlan>.Success(updated.Value);
	}
}
