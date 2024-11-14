using SPerfomance.Domain.Abstractions;

namespace SPerfomance.Domain.Models.Semesters.ValueObjects;

public class SemesterNumber : DomainValueObject
{
    public byte Number { get; init; }

    public static SemesterNumber One = new SemesterNumber(1);

    public static SemesterNumber Two = new SemesterNumber(2);

    public static SemesterNumber Three = new SemesterNumber(3);

    public static SemesterNumber Four = new SemesterNumber(4);

    public static SemesterNumber Five = new SemesterNumber(5);

    public static SemesterNumber Six = new SemesterNumber(6);

    public static SemesterNumber Seven = new SemesterNumber(7);

    public static SemesterNumber Eight = new SemesterNumber(8);

    internal SemesterNumber() { }

    internal SemesterNumber(byte number) => Number = number;

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Number;
    }

    internal static SemesterNumber Create(byte number) => new SemesterNumber(number);

    public int EstimateGroupCourse(Semester semester)
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
