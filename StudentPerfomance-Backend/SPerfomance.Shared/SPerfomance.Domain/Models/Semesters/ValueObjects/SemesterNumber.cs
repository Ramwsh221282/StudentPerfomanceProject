using SPerfomance.Domain.Abstractions;

namespace SPerfomance.Domain.Models.Semesters.ValueObjects;

public class SemesterNumber : DomainValueObject
{
    public byte Number { get; init; }

    public static readonly SemesterNumber One = new SemesterNumber(1);

    public static readonly SemesterNumber Two = new SemesterNumber(2);

    public static readonly SemesterNumber Three = new SemesterNumber(3);

    public static readonly SemesterNumber Four = new SemesterNumber(4);

    public static readonly SemesterNumber Five = new SemesterNumber(5);

    public static readonly SemesterNumber Six = new SemesterNumber(6);

    public static readonly SemesterNumber Seven = new SemesterNumber(7);

    public static readonly SemesterNumber Eight = new SemesterNumber(8);

    internal SemesterNumber() { }

    internal SemesterNumber(byte number) => Number = number;

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Number;
    }

    internal static SemesterNumber Create(byte number) => new SemesterNumber(number);

    public static int EstimateGroupCourse(Semester semester)
    {
        if (semester.Number == Two || semester.Number == One)
            return 1;
        if (semester.Number == Three || semester.Number == Four)
            return 2;
        if (semester.Number == Five || semester.Number == Six)
            return 3;
        return 4;
    }
}
