using SPerfomance.Domain.Module.Shared.Common.Abstractions.Entities;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.EntityValidators;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups.Errors;
using SPerfomance.Domain.Module.Shared.Entities.Students.Validators;
using SPerfomance.Domain.Module.Shared.Entities.Students.ValueObjects;

namespace SPerfomance.Domain.Module.Shared.Entities.Students;

public sealed class Student : Entity
{
	private Student() : base(Guid.Empty)
	{
		Name = StudentName.CreateDefault();
		State = StudentState.CreateDefault();
		Recordbook = StudentRecordBook.CreateDefault();
	}
	private Student(Guid id, StudentName name, StudentState state, StudentRecordBook recordbook, StudentGroup group) : base(id)
	{
		Name = name;
		State = state;
		Recordbook = recordbook;
		Group = group;
	}
	public StudentGroup Group { get; private set; } = null!;
	public StudentName Name { get; private set; } = null!;
	public StudentState State { get; private set; } = null!;
	public StudentRecordBook Recordbook { get; private set; } = null!;

	public CSharpFunctionalExtensions.Result ChangeGroup(StudentGroup? group)
	{
		if (group == null)
			return Failure(new GroupNotFoundError().ToString());
		Group = group;
		return Success();
	}

	public CSharpFunctionalExtensions.Result ChangeName(StudentName name)
	{
		Validator<StudentName> validator = new StudentNameValidator(name);
		if (!validator.Validate())
			return Failure(validator.GetErrorText());
		Name = name;
		return Success();
	}

	public CSharpFunctionalExtensions.Result ChangeState(StudentState state)
	{
		Validator<StudentState> validator = new StudentStateValidator(state);
		if (!validator.Validate())
			return Failure(validator.GetErrorText());
		State = state;
		return Success();
	}

	public CSharpFunctionalExtensions.Result ChangeRecordBook(StudentRecordBook recordBook)
	{
		Validator<StudentRecordBook> validator = new StudentRecordBookValidator(recordBook);
		if (!validator.Validate())
			return Failure(validator.GetErrorText());
		Recordbook = recordBook;
		return Success();
	}

	public static CSharpFunctionalExtensions.Result<Student> Create
	(
		StudentName name,
		StudentState state,
		StudentRecordBook recordBook,
		StudentGroup group)
	{
		Student student = new Student(Guid.NewGuid(), name, state, recordBook, group);
		return student;
	}
}
