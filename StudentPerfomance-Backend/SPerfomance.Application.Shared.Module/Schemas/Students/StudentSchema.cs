using Microsoft.AspNetCore.Mvc;

using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.StudentGroups;
using SPerfomance.DataAccess.Module.Shared.Repositories.Students;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups;
using SPerfomance.Domain.Module.Shared.Entities.Students;
using SPerfomance.Domain.Module.Shared.Entities.Students.ValueObjects;

namespace SPerfomance.Application.Shared.Module.Schemas.Students;

public sealed record StudentSchema : EntitySchema
{
	public string Name { get; init; } = string.Empty;
	public string Surname { get; init; } = string.Empty;
	public string Thirdname { get; init; } = string.Empty;
	public string State { get; init; } = string.Empty;
	public ulong Recordbook { get; init; }
	public StudentsGroupSchema Group { get; init; } = new StudentsGroupSchema();
	public StudentSchema() { }
	public StudentSchema(string? name, string? surname, string? thirdname, string? state, ulong recordBook, StudentsGroupSchema? group)
	{
		if (!string.IsNullOrWhiteSpace(name)) Name = name;
		if (!string.IsNullOrWhiteSpace(surname)) Surname = surname;
		if (!string.IsNullOrWhiteSpace(thirdname)) Thirdname = thirdname;
		if (!string.IsNullOrWhiteSpace(state)) State = state;
		if (recordBook > 0) Recordbook = recordBook;
		if (group != null) Group = group;
	}

	public StudentName CreateName()
	{
		return StudentName.Create(Name, Surname, Thirdname).Value;
	}

	public StudentState CreateState()
	{
		return StudentState.Create(State).Value;
	}

	public StudentRecordBook CreateRecordBook()
	{
		return StudentRecordBook.Create(Recordbook).Value;
	}

	public Student CreateDomainObject(StudentGroup group)
	{
		return Student.Create(CreateName(), CreateState(), CreateRecordBook(), group).Value;
	}
}

public static class StudentSchemaExtensions
{
	public static StudentsRepositoryObject ToRepositoryObject(this StudentSchema student)
	{
		StudentsRepositoryObject parameter = new StudentsRepositoryObject()
		.WithName(student.Name)
		.WithSurname(student.Surname)
		.WithThirdname(student.Thirdname)
		.WithRecordbook(student.Recordbook)
		.WithState(student.State)
		.WithGroup(student.Group.ToRepositoryObject());
		return parameter;
	}

	public static StudentSchema ToSchema(this Student student)
	{
		StudentSchema schema = new StudentSchema(student.Name.Name, student.Name.Surname, student.Name.Thirdname, student.State.State, student.Recordbook.Recordbook, student.Group.ToSchema());
		return schema;
	}

	public static ActionResult<StudentSchema> ToActionResult(this OperationResult<Student> result)
	{
		return result.Result == null || result.IsFailed ?
			new BadRequestObjectResult(result.Error) :
			new OkObjectResult(result.Result.ToSchema());
	}

	public static ActionResult<IReadOnlyCollection<StudentSchema>> ToActionResult(this OperationResult<IReadOnlyCollection<Student>> result)
	{
		return result.Result == null || result.IsFailed ?
			new BadRequestObjectResult(result.Error) :
			new OkObjectResult(result.Result.Select(ToSchema));
	}
}
