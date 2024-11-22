using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.EducationPlans.Errors;
using SPerfomance.Domain.Models.Semesters;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.Semesters.Queries.GetSemester;

public class GetSemesterQueryHandler : IQueryHandler<GetSemesterQuery, Semester>
{
    public async Task<Result<Semester>> Handle(
        GetSemesterQuery command,
        CancellationToken ct = default
    )
    {
        if (command.Plan == null)
            return Result<Semester>.Failure(EducationPlanErrors.NotFoundError());

        return await Task.FromResult(command.Plan.FindSemester(command.SemesterNumber));
    }
}
