using Microsoft.AspNetCore.Mvc;

using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.DataAccess.Module.Shared.Repositories.TeachersDepartments;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments;

namespace SPerfomance.Application.Shared.Module.Schemas.Departments;

public record DepartmentSchema : EntitySchema
{
	public int EntityNumber { get; set; } = 0;
	public string Name { get; init; } = string.Empty;
	public string ShortName { get; init; } = string.Empty;

	public DepartmentSchema() { }

	public DepartmentSchema(string? fullName, string? shortName)
	{
		if (!string.IsNullOrWhiteSpace(fullName)) Name = fullName;
		if (!string.IsNullOrWhiteSpace(shortName)) ShortName = shortName;
	}

	public TeachersDepartment CreateDomainObject() => TeachersDepartment.Create(Name).Value;
}

public static class DepartmentSchemaExtensions
{
	public static DepartmentRepositoryObject ToRepositoryObject(this DepartmentSchema schema)
	{
		DepartmentRepositoryObject parameter = new DepartmentRepositoryObject()
		.WithName(schema.Name)
		.WithShortName(schema.ShortName);
		return parameter;
	}

	public static DepartmentSchema ToSchema(this TeachersDepartment department)
	{
		DepartmentSchema schema = new DepartmentSchema(department.FullName, department.ShortName);
		schema.EntityNumber = department.EntityNumber;
		return schema;
	}

	public static ActionResult<DepartmentSchema> ToActionResult(this OperationResult<TeachersDepartment> result)
	{
		return result.Result == null || result.IsFailed ?
			new BadRequestObjectResult(result.Error) :
			new OkObjectResult(result.Result.ToSchema());
	}

	public static ActionResult<IReadOnlyCollection<DepartmentSchema>> ToActionResult(this OperationResult<IReadOnlyCollection<TeachersDepartment>> result)
	{
		return result.Result == null || result.IsFailed ?
			new BadRequestObjectResult(result.Error) :
			new OkObjectResult(result.Result.Select(ToSchema));
	}
}
