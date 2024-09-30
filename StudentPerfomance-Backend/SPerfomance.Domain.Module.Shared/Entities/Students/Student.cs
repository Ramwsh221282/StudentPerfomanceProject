using SPerfomance.Domain.Module.Shared.Common.Abstractions.Entities;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups;
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

	public void ChangeGroup(StudentGroup group) => Group = group;

	public void ChangeName(StudentName name) => Name = name;

	public void ChangeState(StudentState state) => State = state;

	public void ChangeRecordBook(StudentRecordBook recordBook) => Recordbook = recordBook;

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
