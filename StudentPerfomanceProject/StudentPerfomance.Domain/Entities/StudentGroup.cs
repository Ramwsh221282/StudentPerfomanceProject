using CSharpFunctionalExtensions;

using StudentPerfomance.Domain.Interfaces.Repositories;
using StudentPerfomance.Domain.Validators;
using StudentPerfomance.Domain.ValueObjects.StudentGroup;

namespace StudentPerfomance.Domain.Entities;

public class StudentGroup : Entity
{
	private List<Student> _students = [];
	private StudentGroup() : base(Guid.Empty) { }
	private StudentGroup(Guid id, GroupName name, EducationPlan plan) : base(id)
	{
		Name = name;
		EducationPlan = plan;
	}
	public IReadOnlyCollection<Student> Students => _students;
	public GroupName Name { get; private set; } = null!;
	public EducationPlan EducationPlan { get; private set; } = null!;
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
	public static Result<StudentGroup> Create(GroupName name, EducationPlan plan)
	{
		StudentGroup group = new StudentGroup(Guid.NewGuid(), name, plan);
		Validator<StudentGroup> validator = new StudentGroupValidator(group);
		return validator.Validate() ? group : Result.Failure<StudentGroup>(validator.GetErrorText());
	}
}

public static class StudentGroupExtensions
{
	public static bool IsSameAs(this StudentGroup? group, string? comparable)
	{
		if (group == null || string.IsNullOrWhiteSpace(comparable)) return false;
		return true;
	}

	public static async Task AddStudent(this StudentGroup? group, IRepository<Student> repository, Student student)
	{
		if (group == null || repository == null || student == null) return;
		group.AddStudent(student);
		await repository.Create(student);
	}

	public static async Task RemoveStudent(this StudentGroup? group, IRepository<Student> repository, Student student)
	{
		if (group == null || repository == null || student == null) return;
		group.RemoveStudent(student);
		await repository.Remove(student);
	}

	public static async Task MergeWithGroup(this StudentGroup? group, IRepository<StudentGroup> repository, StudentGroup? other)
	{
		if (group == null || repository == null || other == null) return;
		group.MergeWithGroup(other);
		await repository.Commit();
	}
}
