using SPerfomance.Domain.Module.Shared.Common.Abstractions.EntityValidators;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.ValueObjects;
using SPerfomance.Domain.Module.Shared.Entities.Students.Validators;

namespace SPerfomance.Domain.Module.Shared.Entities.Students.ValueObjects;

public sealed class StudentRecordBook : ValueObject
{
	private StudentRecordBook() { }

	private StudentRecordBook(ulong recordBook) => Recordbook = recordBook;

	public ulong Recordbook { get; }
	public static StudentRecordBook CreateDefault() => new StudentRecordBook();
	public static CSharpFunctionalExtensions.Result<StudentRecordBook> Create(ulong recordBook)
	{
		StudentRecordBook result = new StudentRecordBook(recordBook);
		Validator<StudentRecordBook> validator = new StudentRecordBookValidator(result);
		return validator.Validate() ? result : CSharpFunctionalExtensions.Result.Failure<StudentRecordBook>(validator.GetErrorText());
	}

	public override IEnumerable<object> GetEqualityComponents()
	{
		yield return Recordbook;
	}

	public override string ToString() => Recordbook.ToString();
}
