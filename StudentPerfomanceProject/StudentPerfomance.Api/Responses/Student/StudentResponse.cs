using Microsoft.AspNetCore.Mvc;
using StudentPerfomance.Application;

namespace StudentPerfomance.Api.Responses.Student;

public class StudentResponse
{
	private StudentResponse(string name, string surname, string thirdname, string groupName, string state, ulong recordBook) =>
		 (Name, Surname, Thirdname, GroupName, State, RecordBook) = (name, surname, thirdname, groupName, state, recordBook);

	public string Name { get; }
	public string Surname { get; }
	public string? Thirdname { get; }
	public string GroupName { get; }
	public string State { get; }
	public ulong RecordBook { get; }

	public static StudentResponse FromStudent(Domain.Entities.Student student)
	{
		return new StudentResponse
		(
			 student.Name.Name,
			 student.Name.Surname,
			 student.Name.Thirdname,
			 student.Group.Name.Name,
			 student.State.State,
			 student.Recordbook.Recordbook
		);
	}

	public static ActionResult<IReadOnlyCollection<StudentResponse>> FromResult(OperationResult<IReadOnlyCollection<Domain.Entities.Student>> result) =>
		result.IsFailed ? new BadRequestObjectResult(result.Error) : new OkObjectResult(result.Result.Select(FromStudent));

	public static ActionResult<StudentResponse> FromResult(OperationResult<Domain.Entities.Student> result) =>
		result.IsFailed ? new BadRequestObjectResult(result.Error) : new OkObjectResult(FromStudent(result.Result));
}
