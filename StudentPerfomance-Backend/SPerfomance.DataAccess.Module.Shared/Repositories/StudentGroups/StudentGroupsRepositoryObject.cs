using SPerfomance.DataAccess.Module.Shared.Repositories.EducationPlans;

namespace SPerfomance.DataAccess.Module.Shared.Repositories.StudentGroups;

public sealed class StudentGroupsRepositoryObject
{
	public string Name { get; private set; } = string.Empty;
	public EducationPlansRepositoryObject EducationPlan { get; private set; } = new EducationPlansRepositoryObject();
	public StudentGroupsRepositoryObject WithName(string name)
	{
		if (!string.IsNullOrEmpty(name)) Name = name;
		return this;
	}
	public StudentGroupsRepositoryObject WithPlan(EducationPlansRepositoryObject plan)
	{
		EducationPlan = plan;
		return this;
	}
}
