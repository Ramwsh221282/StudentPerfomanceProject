using Microsoft.AspNetCore.Mvc;
using StudentPerfomance.Api.Responses.Student;
using StudentPerfomance.Application;

namespace StudentPerfomance.Api.Responses.StudentGroup;

public class StudentGroupResponse
{
	private StudentGroupResponse(string groupName, int countOfStudents, IEnumerable<StudentResponse> students) =>
		 (GroupName, StudentsCount, Students) = (groupName, countOfStudents, students);

	public string GroupName { get; }

	public int StudentsCount { get; }

	public IEnumerable<StudentResponse> Students { get; }

	public static StudentGroupResponse FromStudentGroup(Domain.Entities.StudentGroup group)
	{
		return new StudentGroupResponse
		(
			 group.Name.Name,
			 group.Students.Count,
			 group.Students.Select(StudentResponse.FromStudent)
		);
	}

	public static ActionResult<StudentGroupResponse> FromResult(OperationResult<Domain.Entities.StudentGroup> result) =>
		result.IsFailed ? new BadRequestObjectResult(result.Error) : new OkObjectResult(FromStudentGroup(result.Result));

	public static ActionResult<IReadOnlyCollection<StudentGroupResponse>> FromResult(OperationResult<IReadOnlyCollection<Domain.Entities.StudentGroup>> result) =>
		result.IsFailed ? new BadRequestObjectResult(result.Error) : new OkObjectResult(result.Result.Select(FromStudentGroup));
}
