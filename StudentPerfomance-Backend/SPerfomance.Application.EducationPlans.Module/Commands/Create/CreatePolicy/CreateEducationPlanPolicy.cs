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
		int entityNumber = await _repository.Count();
		await GenerateEntityNumbers(entityNumber);
	}

	private async Task GenerateEntityNumbers(int currentCount)
	{
		int entityNumber = currentCount;
		if (entityNumber == 0)
		{
			foreach (var semester in _plan.Semesters)
			{
				semester.SetNumber(entityNumber);
				entityNumber += 1;
			}
		}
		else
		{
			foreach (var semester in _plan.Semesters)
			{
				semester.SetNumber(await _repository.GenerateEntityNumber());
				entityNumber += 1;
			}
		}
	}
}
