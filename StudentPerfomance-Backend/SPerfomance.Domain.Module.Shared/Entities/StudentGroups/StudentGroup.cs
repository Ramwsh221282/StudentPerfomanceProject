using SPerfomance.Domain.Module.Shared.Common.Abstractions.Entities;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups.ValueObjects;
using SPerfomance.Domain.Module.Shared.Entities.Students;

namespace SPerfomance.Domain.Module.Shared.Entities.StudentGroups;

public sealed class StudentGroup : Entity
{
	private List<Student> _students = [];
	private StudentGroup() : base(Guid.Empty)
	{
		Name = GroupName.CreateDefault();
	}
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
	public static CSharpFunctionalExtensions.Result<StudentGroup> Create(GroupName name, EducationPlan plan)
	{
		StudentGroup group = new StudentGroup(Guid.NewGuid(), name, plan);
		return group;
	}
}
