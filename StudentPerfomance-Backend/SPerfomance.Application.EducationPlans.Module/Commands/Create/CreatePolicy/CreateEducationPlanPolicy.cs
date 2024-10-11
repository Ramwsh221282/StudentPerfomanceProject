using SPerfomance.Application.EducationPlans.Module.Repository;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections.ValueObjects;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans;

namespace SPerfomance.Application.EducationPlans.Module.Commands.Create.CreatePolicy;

internal sealed class CreateEducationPlanPolicy(EducationPlan plan) : ICreateEducationPlanPolicy
{
	private readonly EducationPlan _plan = plan;
	private readonly SemesterQueryRepository _repository = new SemesterQueryRepository();
	public async Task ExecutePolicy()
	{
		if (_plan.Direction.Type.Type == DirectionTypeConstraints.BachelorType)
			await new CreateBachelorPolicy(_plan).ExecutePolicy();
		if (_plan.Direction.Type.Type == DirectionTypeConstraints.MagisterType)
			await new CreateMagisterPolicy(_plan).ExecutePolicy();
		int entityNumber = await _repository.Count() + 1;
		foreach (var semester in _plan.Semesters)
		{
			semester.SetNumber(entityNumber);
			entityNumber += 1;
		}
	}
}
