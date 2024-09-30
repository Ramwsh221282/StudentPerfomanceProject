using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;
using StudentPerfomance.Domain.ValueObjects.EducationDirections;
using StudentPerfomance.Domain.ValueObjects.SemesterValueObjects;

namespace StudentPerfomance.Application.Commands.EducationPlans.Create.Decorators.CreateEducationPlanPolicies;

internal sealed class CreateMagisterEducationPlan : ICreateEducationPlanPolicy
{
	private readonly IRepository<Semester> _repository;
	private readonly EducationPlan _plan;
	public CreateMagisterEducationPlan(IRepository<Semester> repository, EducationPlan plan)
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
