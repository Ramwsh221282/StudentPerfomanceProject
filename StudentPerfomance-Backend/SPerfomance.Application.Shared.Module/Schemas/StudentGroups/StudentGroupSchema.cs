using SPerfomance.Application.Shared.Module.Schemas.EducationPlans;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.EntitySchemas;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups.ValueObjects;

namespace SPerfomance.Application.Shared.Module.Schemas.StudentGroups;

public sealed record StudentsGroupSchema : EntitySchema
{
	public string NameInfo { get; private set; } = string.Empty;
	public EducationPlanSchema PlanInfo { get; private set; } = new EducationPlanSchema();
	public StudentsGroupSchema() { }
	public StudentsGroupSchema(string? name, EducationPlanSchema? plan)
	{
		if (!string.IsNullOrWhiteSpace(name)) NameInfo = name;
		if (plan != null) PlanInfo = plan;
	}
	public GroupName CreateGroupName() => GroupName.Create(NameInfo).Value;
}
