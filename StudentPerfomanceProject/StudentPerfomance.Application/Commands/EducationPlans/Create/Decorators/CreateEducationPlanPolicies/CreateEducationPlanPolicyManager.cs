using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;
using StudentPerfomance.Domain.ValueObjects.EducationDirections;

namespace StudentPerfomance.Application.Commands.EducationPlans.Create.Decorators.CreateEducationPlanPolicies;

internal sealed class CreateEducationPlanPolicyManager(EducationPlan plan, IRepository<Semester> repository)
{
	private readonly EducationPlan _plan = plan;
	private readonly IRepository<Semester> _repository = repository;

	public ICreateEducationPlanPolicy CreatePolicy()
	{
		ICreateEducationPlanPolicy policy = _plan.Direction.Type.Type switch
		{
			DirectionTypeConstraints.BachelorType => new CreateBachelorEducationPlan(_repository, _plan),
			DirectionTypeConstraints.MagisterType => new CreateMagisterEducationPlan(_repository, _plan),
			_ => new CreateEducationPlanDefaultPolicy()
		};
		return policy;
	}
}
