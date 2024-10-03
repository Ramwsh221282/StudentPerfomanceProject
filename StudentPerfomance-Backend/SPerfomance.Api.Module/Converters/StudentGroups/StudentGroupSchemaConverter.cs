using SPerfomance.Api.Module.Converters.EducationPlans;
using SPerfomance.Application.Shared.Module.Schemas.StudentGroups;
using SPerfomance.DataAccess.Module.Shared.Repositories.EducationPlans;
using SPerfomance.DataAccess.Module.Shared.Repositories.StudentGroups;

namespace SPerfomance.Api.Module.Converters.StudentGroups;

public static class StudentGroupSchemaConverter
{
	public static StudentGroupsRepositoryObject ToRepositoryObject(this StudentsGroupSchema schema)
	{
		EducationPlansRepositoryObject plan = EducationPlanSchemaConverter.ToRepositoryObject(schema.PlanInfo);
		StudentGroupsRepositoryObject group = new StudentGroupsRepositoryObject()
		.WithName(schema.NameInfo)
		.WithPlan(plan);
		return group;
	}
}
