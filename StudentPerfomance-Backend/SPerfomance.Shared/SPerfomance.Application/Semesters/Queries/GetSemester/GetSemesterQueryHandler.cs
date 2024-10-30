using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.EducationPlans.Errors;
using SPerfomance.Domain.Models.Semesters;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.Semesters.GetSemester.Queries;

public class GetSemesterQueryHandler : IQueryHandler<GetSemesterQuery, Semester>
{
	public async Task<Result<Semester>> Handle(GetSemesterQuery command)
	{
		if (command.Plan == null)
			return Result<Semester>.Failure(EducationPlanErrors.NotFoundError());

		return await Task.FromResult(command.Plan.FindSemester(command.SemesterNumber));
	}
}
