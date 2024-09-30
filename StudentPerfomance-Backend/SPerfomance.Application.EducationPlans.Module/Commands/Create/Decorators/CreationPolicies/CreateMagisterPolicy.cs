using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections.ValueObjects;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans;
using SPerfomance.Domain.Module.Shared.Entities.Semesters;
using SPerfomance.Domain.Module.Shared.Entities.Semesters.ValueObjects;

namespace SPerfomance.Application.EducationPlans.Module.Commands.Create.Decorators.CreationPolicies;

internal sealed class CreateMagisterPolicy : ICreateEducationPlanPolicy
{
	private readonly IRepository<Semester> _repository;
	private readonly EducationPlan _plan;
	public CreateMagisterPolicy(IRepository<Semester> repository, EducationPlan plan)
	{
		_repository = repository;
		_plan = plan;
	}
	public async Task ExecutePolicy()
	{
		Semester[] semesters = Enumerable.Range(1, DirectionTypeConstraints.MagisterSemestersLimit)
		.Select(s => Semester.Create(SemesterNumber.Create((byte)s).Value, _plan).Value)
		.ToArray();
		int entityNumber = await _repository.Count();
		entityNumber += 1;
		foreach (var semester in semesters)
		{
			semester.SetNumber(entityNumber);
			_plan.AddSemester(semester);
			entityNumber++;
		}
	}
}
