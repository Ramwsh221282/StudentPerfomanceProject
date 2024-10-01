using Microsoft.AspNetCore.Mvc;

using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments;

namespace SPerfomance.Api.Module.Responses.TeacherDepartments;

public sealed class TeacherDepartmentResponse
{
	public string FullName { get; init; }
	public string ShortName { get; init; }
	public int TeachersCount { get; init; }
	public int EntityNumber { get; init; }
	public TeacherDepartmentResponse(TeachersDepartment department)
	{
		FullName = department.FullName;
		ShortName = department.ShortName;
		TeachersCount = department.Teachers.Count;
		EntityNumber = department.EntityNumber;
	}

	public static TeacherDepartmentResponse FromDepartment(TeachersDepartment department) => new TeacherDepartmentResponse(department);

	public static ActionResult<TeacherDepartmentResponse> FromResult(OperationResult<TeachersDepartment> result) =>
		result.Result == null || result.IsFailed ?
			new BadRequestObjectResult(result.Error) :
			new OkObjectResult(FromDepartment(result.Result));

	public static ActionResult<IReadOnlyCollection<TeacherDepartmentResponse>> FromResult(OperationResult<IReadOnlyCollection<TeachersDepartment>> result) =>
		result.Result == null || result.IsFailed ?
			new BadRequestObjectResult(result.Error)
		 	: new OkObjectResult(result.Result.Select(FromDepartment));
}
