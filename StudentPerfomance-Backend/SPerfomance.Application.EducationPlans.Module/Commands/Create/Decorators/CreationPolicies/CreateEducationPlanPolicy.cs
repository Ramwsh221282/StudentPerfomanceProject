using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections.ValueObjects;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans;
using SPerfomance.Domain.Module.Shared.Entities.Semesters;

namespace SPerfomance.Application.EducationPlans.Module.Commands.Create.Decorators.CreationPolicies;

internal sealed class CreateEducationPlanPolicy(EducationPlan plan, IRepository<Semester> repository) : ICreateEducationPlanPolicy
{
	private readonly EducationPlan _plan = plan;
	private readonly IRepository<Semester> _repository = repository;

	public async Task ExecutePolicy()
	{
		if (_plan.Direction.Type.Type == DirectionTypeConstraints.BachelorType)
			await new CreateBachelorPolicy(_repository, _plan).ExecutePolicy();
		if (_plan.Direction.Type.Type == DirectionTypeConstraints.MagisterType)
			await new CreateMagisterPolicy(_repository, _plan).ExecutePolicy();
	}
}
