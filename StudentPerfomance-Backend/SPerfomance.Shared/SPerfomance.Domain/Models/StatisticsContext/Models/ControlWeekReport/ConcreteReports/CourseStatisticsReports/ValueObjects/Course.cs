using SPerfomance.Domain.Abstractions;
using SPerfomance.Domain.Models.EducationDirections.ValueObjects;
using SPerfomance.Domain.Models.Semesters;
using SPerfomance.Domain.Models.Semesters.ValueObjects;
using SPerfomance.Domain.Models.StudentGroups;

namespace SPerfomance.Domain.Models.StatisticsContext.Models.ControlWeekReport.ConcreteReports.CourseStatisticsReports.ValueObjects;

public sealed class Course : DomainValueObject
{
    internal Course(Semester semester) =>
        Value = (byte)Semesters.ValueObjects.SemesterNumber.EstimateGroupCourse(semester);

    internal Course(byte number) => Value = number;

    public byte Value { get; init; }

    public static Course CreateFromGroup(StudentGroup group)
    {
        if (group.ActiveGroupSemester == null)
            return new Course(Semester.Empty);

        return group.ActiveGroupSemester.Plan.Direction.Type == DirectionType.Bachelor
            ? new Course(EstimateBachelorGroupCousre(group.ActiveGroupSemester.Number))
            : new Course(EstimateMagisterGroupCousre(group.ActiveGroupSemester.Number));
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    private static byte EstimateBachelorGroupCousre(SemesterNumber activeSemesterNumber)
    {
        if (
            activeSemesterNumber == SemesterNumber.One
            || activeSemesterNumber == SemesterNumber.Two
        )
            return 1;
        if (
            activeSemesterNumber == SemesterNumber.Three
            || activeSemesterNumber == SemesterNumber.Four
        )
            return 2;
        if (
            activeSemesterNumber == SemesterNumber.Five
            || activeSemesterNumber == SemesterNumber.Six
        )
            return 3;
        if (
            activeSemesterNumber == SemesterNumber.Seven
            || activeSemesterNumber == SemesterNumber.Eight
        )
            return 4;
        return 0;
    }

    private static byte EstimateMagisterGroupCousre(SemesterNumber activeSemesterNumber)
    {
        if (
            activeSemesterNumber == SemesterNumber.One
            || activeSemesterNumber == SemesterNumber.Two
        )
            return 1;
        if (
            activeSemesterNumber == SemesterNumber.Three
            || activeSemesterNumber == SemesterNumber.Four
        )
            return 2;
        return 0;
    }
}
