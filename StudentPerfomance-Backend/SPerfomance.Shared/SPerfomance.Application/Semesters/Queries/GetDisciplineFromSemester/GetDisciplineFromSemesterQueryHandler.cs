using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.SemesterPlans;
using SPerfomance.Domain.Models.Semesters.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.Semesters.Queries.GetDisciplineFromSemester;

public class GetDisciplineFromSemesterQueryHandler : IQueryHandler<GetDisciplineFromSemesterQuery, SemesterPlan>
{
	public async Task<Result<SemesterPlan>> Handle(GetDisciplineFromSemesterQuery command)
	{
		if (command.Semester == null)
			return Result<SemesterPlan>.Failure(SemesterErrors.NotFound());

		return await Task.FromResult(command.Semester.FindDiscipline(command.Name));
	}
}
