using SPerfomance.Application.Shared.Module.Schemas.Departments;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.EntitySchemas;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments;
using SPerfomance.Domain.Module.Shared.Entities.Teachers;
using SPerfomance.Domain.Module.Shared.Entities.Teachers.ValueObjects;

namespace SPerfomance.Application.Shared.Module.Schemas.Teachers;

public sealed record TeacherSchema : EntitySchema
{
	public string Name { get; init; } = string.Empty;
	public string Surname { get; init; } = string.Empty;
	public string Thirdname { get; init; } = string.Empty;
	public string Condition { get; init; } = string.Empty;
	public string Job { get; init; } = string.Empty;
	public DepartmentSchema Department { get; init; } = new DepartmentSchema();
	public TeacherSchema() { }
	public TeacherSchema(string? name, string? surname, string? thirdname, string? condition, string? job, DepartmentSchema? department)
	{
		if (!string.IsNullOrWhiteSpace(name)) Name = name;
		if (!string.IsNullOrWhiteSpace(surname)) Surname = surname;
		if (!string.IsNullOrWhiteSpace(thirdname)) Thirdname = thirdname;
		if (!string.IsNullOrWhiteSpace(condition)) Condition = condition;
		if (!string.IsNullOrWhiteSpace(job)) Job = job;
		if (department != null) Department = department;
	}

	public TeacherName CreateName()
	{
		return TeacherName.Create(Name, Surname, Thirdname).Value;
	}

	public WorkingCondition CreateWorkingCondition()
	{
		return WorkingCondition.Create(Condition).Value;
	}

	public JobTitle CreateJobTitle()
	{
		return JobTitle.Create(Job).Value;
	}

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
