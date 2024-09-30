using SPerfomance.Domain.Module.Shared.Common.Abstractions.EntityValidators;
using SPerfomance.Domain.Module.Shared.Entities.Students.Errors;
using SPerfomance.Domain.Module.Shared.Entities.Students.ValueObjects;

namespace SPerfomance.Domain.Module.Shared.Entities.Students.Validators;

internal sealed class StudentRecordBookValidator(StudentRecordBook recordBook) : Validator<StudentRecordBook>
{
	private readonly StudentRecordBook _recordBook = recordBook;
	public override bool Validate()
	{
		if (_recordBook.Recordbook <= 0)
			error.AppendError(new StudentRecordBookError());
		return HasError;
	}
}
