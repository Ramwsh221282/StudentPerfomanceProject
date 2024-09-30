using Microsoft.AspNetCore.Mvc;
using StudentPerfomance.Application;
using StudentPerfomance.Domain.Entities;

namespace StudentPerfomance.Api.Responses.Semesters;

public sealed class SemesterResponse
{
	private SemesterResponse(int number, int contractsCount) =>
		 (Number, ContractsCount) = (number, contractsCount);

	public int Number { get; }
	public int ContractsCount { get; }

	public static SemesterResponse FromSemester(Semester semester)
	{
		return new SemesterResponse
		(
			 semester.Number.Value,
			 semester.Contracts.Count
		);
	}

	public static ActionResult<IReadOnlyCollection<SemesterResponse>> FromResult(OperationResult<IReadOnlyCollection<Semester>> result) =>
		result.IsFailed ? new BadRequestObjectResult(result.Error) : new OkObjectResult(result.Result.Select(FromSemester));

	public static ActionResult<SemesterResponse> FromResult(OperationResult<Semester> result) =>
		result.IsFailed ? new BadRequestObjectResult(result.Error) : new OkObjectResult(FromSemester(result.Result));
}
