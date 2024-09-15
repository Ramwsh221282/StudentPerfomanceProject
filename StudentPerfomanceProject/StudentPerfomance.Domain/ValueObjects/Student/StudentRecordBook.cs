using CSharpFunctionalExtensions;
using StudentPerfomance.Domain.Validators;
using StudentPerfomance.Domain.Validators.Student;

namespace StudentPerfomance.Domain.ValueObjects.Student;

public class StudentRecordBook : ValueObject
{
	private StudentRecordBook() { }

	private StudentRecordBook(ulong recordBook) => Recordbook = recordBook;

	public ulong Recordbook { get; }

	public static Result<StudentRecordBook> Create(ulong recordBook)
	{
		Validator<StudentRecordBook> validator = new StudentRecordBookValidator(recordBook);
		if (!validator.Validate())
			return Result.Failure<StudentRecordBook>(validator.GetErrorText());
		return new StudentRecordBook(recordBook);
	}

	public override IEnumerable<object> GetEqualityComponents()
	{
		yield return Recordbook;
	}
}

public static class StudentRecordbookExtensions
{
	public static bool IsSameAs(this StudentRecordBook? recordBook, StudentRecordBook? comparable)
	{
		if (recordBook == null || comparable == null) return false;
		return recordBook.Recordbook == comparable.Recordbook;
	}

	public static bool IsSameAs(this StudentRecordBook? recordBook, ulong comparable)
	{
		if (recordBook == null || comparable <= 0) return false;
		return recordBook.Recordbook == comparable;
	}
}
