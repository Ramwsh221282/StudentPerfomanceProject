using SPerfomance.Domain.Module.Shared.Entities.EducationDirections.ValueObjects;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans;
using SPerfomance.Domain.Module.Shared.Entities.Semesters;
using SPerfomance.Domain.Module.Shared.Entities.Semesters.ValueObjects;

namespace SPerfomance.Application.EducationPlans.Module.Commands.Create.CreatePolicy;

internal sealed class CreateBachelorPolicy : ICreateEducationPlanPolicy
{
	private readonly EducationPlan _plan;
	public CreateBachelorPolicy(EducationPlan plan)
	{
		_plan = plan;
	}
	public async Task ExecutePolicy()
	{
		await Task.Run(() =>
		{
			Semester[] semesters = Enumerable.Range(1, DirectionTypeConstraints.BachelorSemestersLimit)
			.Select(s => Semester.Create(SemesterNumber.Create((byte)s).Value, _plan).Value)
			.ToArray();
			foreach (var semester in semesters)
			{
				_plan.AddSemester(semester);
			}
		});
	}
}
