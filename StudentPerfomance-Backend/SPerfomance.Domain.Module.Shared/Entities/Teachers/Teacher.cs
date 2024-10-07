using SPerfomance.Domain.Module.Shared.Common.Abstractions.Entities;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.EntityValidators;
using SPerfomance.Domain.Module.Shared.Entities.SemesterPlans;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments;
using SPerfomance.Domain.Module.Shared.Entities.Teachers.Validators;
using SPerfomance.Domain.Module.Shared.Entities.Teachers.ValueObjects;

namespace SPerfomance.Domain.Module.Shared.Entities.Teachers;

public sealed class Teacher : Entity
{
	private List<SemesterPlan> _disciplines = [];

	private Teacher() : base(Guid.Empty)
	{
		Name = TeacherName.CreateDefault();
		Condition = WorkingCondition.CreateDefault();
		JobTitle = JobTitle.CreateDefault();
	}

	private Teacher(Guid id, TeacherName name, WorkingCondition condition, JobTitle jobTitle, TeachersDepartment department) : base(id)
	{
		Name = name;
		Department = department;
		Condition = condition;
		JobTitle = jobTitle;
	}

	public WorkingCondition Condition { get; private set; } = null!;
	public JobTitle JobTitle { get; private set; } = null!;
	public TeacherName Name { get; private set; }
	public TeachersDepartment Department { get; } = null!;
	public IReadOnlyCollection<SemesterPlan> Disciplines => _disciplines.AsReadOnly();
	public CSharpFunctionalExtensions.Result ChangeName(TeacherName newName)
	{
		var validator = new TeacherNameValidator(newName.Name, newName.Surname, newName.Thirdname);
		if (!validator.Validate())
			return Failure(validator.GetErrorText());
		Name = newName;
		return Success();
	}
	public CSharpFunctionalExtensions.Result ChangeCondition(WorkingCondition condition)
	{
		Validator<WorkingCondition> validator = new WorkingConditionValidator(condition);
		if (!validator.Validate())
			return Failure(validator.GetErrorText());
		Condition = condition;
		return Success();
	}
	public CSharpFunctionalExtensions.Result ChangeJobTitle(JobTitle jobTitle)
	{
		Validator<JobTitle> validator = new JobTitleValidator(jobTitle);
		if (!validator.Validate())
			return Failure(validator.GetErrorText());
		JobTitle = jobTitle;
		return Success();
	}

	public static CSharpFunctionalExtensions.Result<Teacher> Create
	(
		TeacherName name,
		WorkingCondition condition,
		JobTitle jobTitle,
		TeachersDepartment department
	)
	{
		Teacher teacher = new Teacher(Guid.NewGuid(), name, condition, jobTitle, department);
		return teacher;
	}
}
