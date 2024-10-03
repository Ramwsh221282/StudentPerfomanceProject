using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Module.Responses.StudentGroups;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.Students;

namespace SPerfomance.Api.Module.Responses.Students;

public sealed class StudentResponse
{
	public string Name { get; init; }
	public string Surname { get; init; }
	public string Thirdname { get; init; }
	public string State { get; init; }
	public ulong Recordbook { get; init; }
	public StudentGroupResponse Group { get; init; }
	private StudentResponse(Student student)
	{
		Name = student.Name.Name;
		Surname = student.Name.Surname;
		Thirdname = student.Name.Thirdname;
		State = student.State.State;
		Recordbook = student.Recordbook.Recordbook;
		Group = StudentGroupResponse.FromStudentGroup(student.Group);
	}

	public static StudentResponse FromStudent(Student student) => new StudentResponse(student);
	public static ActionResult<StudentResponse> FromResult(OperationResult<Student> result) =>
	result.Result == null || result.IsFailed ? new BadRequestObjectResult(result.Error) : new OkObjectResult(FromStudent(result.Result));

	public static ActionResult<IReadOnlyCollection<StudentResponse>> FromResult(OperationResult<IReadOnlyCollection<Student>> result) =>
	result.Result == null || result.IsFailed ? new BadRequestObjectResult(result.Error) : new OkObjectResult(result.Result.Select(FromStudent));
}
