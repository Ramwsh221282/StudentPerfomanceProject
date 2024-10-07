using SPerfomance.Domain.Module.Shared.Entities.EducationDirections.ValueObjects;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans;
using SPerfomance.Domain.Module.Shared.Entities.Semesters;
using SPerfomance.Domain.Module.Shared.Entities.Semesters.ValueObjects;

namespace SPerfomance.Application.EducationPlans.Module.Commands.Create.CreatePolicy;

internal sealed class CreateMagisterPolicy : ICreateEducationPlanPolicy
{
	private readonly EducationPlan _plan;
	public CreateMagisterPolicy(EducationPlan plan)
	{
		_plan = plan;
	}
	public async Task ExecutePolicy()
	{
		await Task.Run(() =>
		{
			Semester[] semesters = Enumerable.Range(1, DirectionTypeConstraints.MagisterSemestersLimit)
			.Select(s => Semester.Create(SemesterNumber.Create((byte)s).Value, _plan).Value)
			.ToArray();
			foreach (var semester in semesters)
			{
				_plan.AddSemester(semester);
			}
		});
	}
}
