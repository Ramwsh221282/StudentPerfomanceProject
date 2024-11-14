using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.EducationPlans;
using SPerfomance.Domain.Models.Semesters;

namespace SPerfomance.Application.Semesters.GetSemester.Queries;

public class GetSemesterQuery(EducationPlan? plan, byte? semesterNumber) : IQuery<Semester>
{
    public EducationPlan? Plan { get; init; } = plan;

    public byte SemesterNumber { get; init; } = semesterNumber.GetValueOrDefault();
}
