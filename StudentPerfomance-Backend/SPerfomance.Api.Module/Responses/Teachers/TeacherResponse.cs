using Microsoft.AspNetCore.Mvc;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.Teachers;

namespace SPerfomance.Api.Module.Responses.Teachers;

public sealed class TeacherResponse
{
	public string Name { get; init; }
	public string Surname { get; init; }
	public string Thirdname { get; init; }
	public string JobTitle { get; init; }
	public string WorkingCondition { get; init; }
	private TeacherResponse(Teacher teacher)
	{
		Name = teacher.Name.Name;
		Surname = teacher.Name.Surname;
		Thirdname = teacher.Name.Thirdname;
		JobTitle = teacher.JobTitle.Value;
		WorkingCondition = teacher.Condition.Value;
	}

	public static TeacherResponse FromTeacher(Teacher teacher) => new TeacherResponse(teacher);
	public static ActionResult<TeacherResponse> FromResult(OperationResult<Teacher> result) =>
	result.Result == null || result.IsFailed ? new BadRequestObjectResult(result.Error) : new OkObjectResult(FromTeacher(result.Result));

	public static ActionResult<IReadOnlyCollection<TeacherResponse>> FromResult(OperationResult<IReadOnlyCollection<Teacher>> result) =>
	result.Result == null || result.IsFailed ? new BadRequestObjectResult(result.Error) : new OkObjectResult(result.Result.Select(FromTeacher));
}
