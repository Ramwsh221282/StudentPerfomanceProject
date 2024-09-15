using Microsoft.AspNetCore.Mvc;
using StudentPerfomance.Application;
using StudentPerfomance.Domain.Entities;

namespace StudentPerfomance.Api.Responses.TeacherDepartments;

public class TeacherDepartmentResponse
{
	private TeacherDepartmentResponse(string name, int teachersCount) =>
		(Name, TeachersCount) = (name, teachersCount);

	public string Name { get; }

	public int TeachersCount { get; }

	private static TeacherDepartmentResponse FromDepartment(TeachersDepartment department)
	{
		return new TeacherDepartmentResponse
		(
			department.Name,
			department.Teachers.Count
		);
	}

	public static ActionResult<TeacherDepartmentResponse> FromResult(OperationResult<TeachersDepartment> result) =>
		result.IsFailed ? new BadRequestObjectResult(result.Error) : new OkObjectResult(FromDepartment(result.Result));

	public static ActionResult<IReadOnlyCollection<TeacherDepartmentResponse>> FromResult(OperationResult<IReadOnlyCollection<TeachersDepartment>> result) =>
		result.IsFailed ? new BadRequestObjectResult(result.Error) : new OkObjectResult(result.Result.Select(FromDepartment));
}
