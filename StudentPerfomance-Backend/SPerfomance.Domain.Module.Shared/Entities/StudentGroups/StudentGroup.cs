using SPerfomance.Domain.Module.Shared.Common.Abstractions.Entities;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.EntityValidators;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups.Errors;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups.Validators;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups.ValueObjects;
using SPerfomance.Domain.Module.Shared.Entities.Students;
using SPerfomance.Domain.Module.Shared.Entities.Students.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.StudentGroups;

public sealed class StudentGroup : Entity
{
	private List<Student> _students = [];
	private StudentGroup() : base(Guid.Empty)
	{
		Name = GroupName.CreateDefault();
	}
	private StudentGroup(Guid id, GroupName name) : base(id)
	{
		Name = name;
	}
	public IReadOnlyCollection<Student> Students => _students;
	public GroupName Name { get; private set; } = null!;
	public EducationPlan? EducationPlan { get; private set; }
	public CSharpFunctionalExtensions.Result ChangeGroupName(GroupName name)
	{
		Validator<GroupName> validator = new GroupNameValidator(name);
		if (!validator.Validate()) return Failure(validator.GetErrorText());
		if (Name == name) return Failure(new GroupSameNameError().ToString());
		Name = name;
		return Success();
	}
	public CSharpFunctionalExtensions.Result AddStudent(Student student)
	{
		if (_students.Any(s => s.Id == student.Id &&
			s.Name == student.Name &&
			s.Recordbook == student.Recordbook))
			return Failure(new GroupHasStudentAlreadyError().ToString());
		if (_students.Any(s => s.Recordbook == student.Recordbook))
			return Failure(new StudentDublicateRecordBookError(student.Recordbook.Recordbook).ToString());
		student.ChangeGroup(this);
		_students.Add(student);
		return Success();
	}
	public CSharpFunctionalExtensions.Result RemoveStudent(Student student)
	{
		if (student == null)
			return Failure(new StudentNotFoundError().ToString());
		Student? targetStudent = _students.FirstOrDefault(s => s.Id == student.Id);
		if (targetStudent == null)
			return Failure(new StudentNotFoundError().ToString());
		_students.Remove(targetStudent);
		return Success();
	}
	public CSharpFunctionalExtensions.Result MergeWithGroup(StudentGroup? mergeGroup)
	{
		if (mergeGroup == null)
			return Failure(new GroupNotFoundError().ToString());
		mergeGroup._students.ForEach(s => s.ChangeGroup(this));
		_students.AddRange(mergeGroup._students);
		mergeGroup._students.Clear();
		return Success();
	}

	public CSharpFunctionalExtensions.Result AttachEducationPlan(EducationPlan plan)
	{
		if (EducationPlan != null)
			return Failure(new GroupPlanAlreadyAttachedError().ToString());
		EducationPlan = plan;
		return Success();
	}

	public CSharpFunctionalExtensions.Result DeattachEducationPlan()
	{
		if (EducationPlan == null)
			return Failure(new GroupPlanAttachmentError().ToString());
		EducationPlan = null;
		return Success();
	}

	public static CSharpFunctionalExtensions.Result<StudentGroup> Create(GroupName name)
	{
		StudentGroup group = new StudentGroup(Guid.NewGuid(), name);
		return group;
	}
}
