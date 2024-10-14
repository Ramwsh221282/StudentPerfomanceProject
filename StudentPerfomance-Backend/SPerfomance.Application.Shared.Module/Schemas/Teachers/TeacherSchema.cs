using Microsoft.AspNetCore.Mvc;

using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.Departments;
using SPerfomance.DataAccess.Module.Shared.Repositories.Teachers;
using SPerfomance.DataAccess.Module.Shared.Repositories.TeachersDepartments;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments;
using SPerfomance.Domain.Module.Shared.Entities.Teachers;
using SPerfomance.Domain.Module.Shared.Entities.Teachers.ValueObjects;

namespace SPerfomance.Application.Shared.Module.Schemas.Teachers;

public sealed record TeacherSchema : EntitySchema
{
	public string Name { get; init; } = string.Empty;
	public string Surname { get; init; } = string.Empty;
	public string Thirdname { get; init; } = string.Empty;
	public string WorkingCondition { get; init; } = string.Empty;
	public string JobTitle { get; init; } = string.Empty;
	public DepartmentSchema Department { get; init; } = new DepartmentSchema();

	public TeacherSchema() { }

	public TeacherSchema(string? name, string? surname, string? thirdname, string? condition, string? job, DepartmentSchema? department)
	{
		if (!string.IsNullOrWhiteSpace(name)) Name = name;
		if (!string.IsNullOrWhiteSpace(surname)) Surname = surname;
		if (!string.IsNullOrWhiteSpace(thirdname)) Thirdname = thirdname;
		if (!string.IsNullOrWhiteSpace(condition)) WorkingCondition = condition;
		if (!string.IsNullOrWhiteSpace(job)) JobTitle = job;
		if (department != null) Department = department;
	}

	public TeacherName CreateName() => TeacherName.Create(Name, Surname, Thirdname).Value;

	public WorkingCondition CreateWorkingCondition() =>
		Domain.Module.Shared.Entities.Teachers.ValueObjects.WorkingCondition.Create(WorkingCondition).Value;

	public JobTitle CreateJobTitle() =>
		Domain.Module.Shared.Entities.Teachers.ValueObjects.JobTitle.Create(JobTitle).Value;

	public Teacher CreateDomainObject(TeachersDepartment department)
	{
		return Teacher.Create
		(
			CreateName(),
			CreateWorkingCondition(),
			CreateJobTitle(),
			department
		).Value;
	}
}

public static class TeacherSchemaExtensions
{
	public static TeacherRepositoryObject ToRepositoryObject(this TeacherSchema teacher)
	{
		DepartmentRepositoryObject department = teacher.Department.ToRepositoryObject();
		TeacherRepositoryObject parameter = new TeacherRepositoryObject()
		.WithName(teacher.Name)
		.WithSurname(teacher.Surname)
		.WithThirdname(teacher.Thirdname)
		.WithJobTitle(teacher.JobTitle)
		.WithWorkingCondition(teacher.WorkingCondition)
		.WithDepartment(department);
		return parameter;
	}

	public static TeacherSchema ToSchema(this Teacher teacher)
	{
		DepartmentSchema department = teacher.Department.ToSchema();
		TeacherSchema schema = new TeacherSchema(
			teacher.Name.Name,
			teacher.Name.Surname,
			teacher.Name.Thirdname,
			teacher.Condition.Value,
			teacher.JobTitle.Value,
			department
		);

		return schema;
	}

	public static ActionResult<TeacherSchema> ToActionResult(this OperationResult<Teacher> result)
	{
		return result.Result == null || result.IsFailed ?
			new BadRequestObjectResult(result.Error) :
			new OkObjectResult(result.Result.ToSchema());
	}

	public static ActionResult<IReadOnlyCollection<TeacherSchema>> ToActionResult(this OperationResult<IReadOnlyCollection<Teacher>> result)
	{
		return result.Result == null || result.IsFailed ?
			new BadRequestObjectResult(result.Error) :
			new OkObjectResult(result.Result.Select(ToSchema));
	}
}
