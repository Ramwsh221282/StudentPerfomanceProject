using Microsoft.AspNetCore.Mvc;

using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.EducationPlans;
using SPerfomance.DataAccess.Module.Shared.Repositories.StudentGroups;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups.ValueObjects;

namespace SPerfomance.Application.Shared.Module.Schemas.StudentGroups;

public sealed record StudentsGroupSchema : EntitySchema
{
	public string Name { get; private set; } = string.Empty;
	public EducationPlanSchema Plan { get; private set; } = new EducationPlanSchema();
	public StudentsGroupSchema() { }
	public StudentsGroupSchema(string? name, EducationPlanSchema? plan)
	{
		if (!string.IsNullOrWhiteSpace(name)) Name = name;
		if (plan != null) Plan = plan;
	}
	public GroupName CreateGroupName() => GroupName.Create(Name).Value;
	public StudentGroup CreateDomainObject() => StudentGroup.Create(CreateGroupName()).Value;
}

public static class StudentGroupSchemaExtensions
{
	public static StudentGroupsRepositoryObject ToRepositoryObject(this StudentsGroupSchema schema)
	{
		StudentGroupsRepositoryObject parameter = new StudentGroupsRepositoryObject()
		.WithName(schema.Name)
		.WithPlan(schema.Plan.ToRepositoryObject());
		return parameter;
	}

	public static StudentsGroupSchema ToSchema(this StudentGroup group)
	{
		EducationPlanSchema plan;
		if (group.EducationPlan == null)
			plan = new EducationPlanSchema();
		else
			plan = group.EducationPlan.ToSchema();
		StudentsGroupSchema schema = new StudentsGroupSchema(group.Name.Name, plan);
		return schema;
	}

	public static ActionResult<StudentsGroupSchema> ToActionResult(this OperationResult<StudentGroup> result)
	{
		return result.Result == null || result.IsFailed ?
			new BadRequestObjectResult(result.Error) :
			new OkObjectResult(result.Result.ToSchema());
	}

	public static ActionResult<IReadOnlyCollection<StudentsGroupSchema>> ToActionResult(this OperationResult<IReadOnlyCollection<StudentGroup>> result)
	{
		return result.Result == null || result.IsFailed ?
			new BadRequestObjectResult(result.Error) :
			new OkObjectResult(result.Result.Select(ToSchema));
	}
}
