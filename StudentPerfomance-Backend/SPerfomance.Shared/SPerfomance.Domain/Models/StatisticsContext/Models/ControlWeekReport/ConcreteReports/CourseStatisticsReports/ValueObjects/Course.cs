using SPerfomance.Domain.Abstractions;
using SPerfomance.Domain.Models.Semesters;

namespace SPerfomance.Domain.Models.StatisticsContext.Models.ControlWeekReport.ConcreteReports.CourseStatisticsReports.ValueObjects;

public sealed class Course : DomainValueObject
{
    internal Course(Semester semester) =>
        Value = (byte)semester.Number.EstimateGroupCourse(semester);

    public byte Value { get; init; }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
