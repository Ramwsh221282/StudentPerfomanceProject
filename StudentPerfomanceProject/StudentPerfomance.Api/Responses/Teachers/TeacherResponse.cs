using Microsoft.AspNetCore.Mvc;
using StudentPerfomance.Application;
using StudentPerfomance.Domain.Entities;

namespace StudentPerfomance.Api.Responses.Teachers;

public class TeacherResponse
{
	private TeacherResponse(string departmentName, string name, string surname, string thirdname) =>
		(DepartmentName, Name, Surname, Thirdname) = (departmentName, name, surname, thirdname);

	public string DepartmentName { get; }
	public string Name { get; }
	public string Surname { get; }
	public string Thirdname { get; }

	public static TeacherResponse FromTeacher(Teacher teacher)
	{
		return new TeacherResponse
		(
			teacher.Department.Name,
			teacher.Name.Name,
			teacher.Name.Surname,
			teacher.Name.Thirdname
		);
	}

	public static ActionResult<TeacherResponse> FromResult(OperationResult<Teacher> result) =>
		result.IsFailed ? new BadRequestObjectResult(result.Error) : new OkObjectResult(FromTeacher(result.Result));

	public static ActionResult<IReadOnlyCollection<TeacherResponse>> FromResult(OperationResult<IReadOnlyCollection<Teacher>> result) =>
		result.IsFailed ? new BadRequestObjectResult(result.Error) : new OkObjectResult(result.Result.Select(FromTeacher));
}
