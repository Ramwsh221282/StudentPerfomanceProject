using SPerfomance.Domain.Abstractions;
using SPerfomance.Domain.Models.Students.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Domain.Models.Students.ValueObjects;

public class StudentRecordbook : DomainValueObject
{
    public ulong Recordbook { get; }

    private StudentRecordbook() { }

    private StudentRecordbook(ulong recordbook) => Recordbook = recordbook;

    internal static StudentRecordbook Empty => new StudentRecordbook();

    internal static Result<StudentRecordbook> Create(ulong recordBook)
    {
        return recordBook <= 0
            ? Result<StudentRecordbook>.Failure(StudentErrors.InvalidRecordbook(recordBook))
            : Result<StudentRecordbook>.Success(new StudentRecordbook(recordBook));
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Recordbook;
    }
}
