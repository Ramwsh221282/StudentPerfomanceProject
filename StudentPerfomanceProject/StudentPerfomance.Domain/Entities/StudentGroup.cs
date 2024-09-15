using CSharpFunctionalExtensions;
using StudentPerfomance.Domain.Validators;
using StudentPerfomance.Domain.ValueObjects.StudentGroup;

namespace StudentPerfomance.Domain.Entities;

public class StudentGroup : Entity
{
	private List<Student> _students = [];
	private StudentGroup() : base(Guid.Empty) { }
	private StudentGroup(Guid id, GroupName name) : base(id) => Name = name;
	public IReadOnlyCollection<Student> Students
	{
		get => _students.AsReadOnly();
	}
	public GroupName Name { get; private set; } = null!;

	public void ChangeGroupName(GroupName name) => Name = name;

	public void AddStudent(Student student) => _students.Add(student);

	public void RemoveStudent(Student student)
	{
		Student? targetStudent = _students.FirstOrDefault(s => s.Id == student.Id);
		if (targetStudent != null)
			_students.Remove(targetStudent);
	}

	public void MergeWithGroup(StudentGroup mergeGroup)
	{
		mergeGroup._students.ForEach(s => s.ChangeGroup(this));
		_students.AddRange(mergeGroup._students);
		mergeGroup._students.Clear();
	}

	public static Result<StudentGroup> Create(Guid id, GroupName name)
	{
		StudentGroup group = new StudentGroup(id, name);
		Validator<StudentGroup> validator = new StudentGroupValidator(group);
		return validator.Validate() switch
		{
			true => group,
			false => Result.Failure<StudentGroup>(validator.GetErrorText())
		};
	}
}

public static class StudentGroupExtensions
{
	public static bool IsSameAs(this StudentGroup? group, string? comparable)
	{
		if (group == null || string.IsNullOrWhiteSpace(comparable)) return false;
		return true;
	}
}
