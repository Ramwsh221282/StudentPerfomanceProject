using SPerfomance.Domain.Abstractions;
using SPerfomance.Domain.Models.TeacherDepartments.ValueObjects;
using SPerfomance.Domain.Models.Teachers;
using SPerfomance.Domain.Models.Teachers.Errors;
using SPerfomance.Domain.Models.Teachers.ValueObjects;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Domain.Models.TeacherDepartments;

public class TeachersDepartments : AggregateRoot
{
	private readonly List<Teacher> _teachers = [];

	public DepartmentName Name { get; private set; }

	public string Acronymus { get; private set; }

	public IReadOnlyCollection<Teacher> Teachers => _teachers;

	private TeachersDepartments() : base(Guid.Empty)
	{
		Name = DepartmentName.Empty;
		Acronymus = string.Empty;
	}

	private TeachersDepartments(DepartmentName name) : base(Guid.NewGuid())
	{
		Name = name;
		Acronymus = name.Name.CreateAcronymus();
	}

	internal static TeachersDepartments Empty => new TeachersDepartments();

	public static Result<TeachersDepartments> Create(string name)
	{
		Result<DepartmentName> nameResult = DepartmentName.Create(name);
		if (nameResult.IsFailure)
			return Result<TeachersDepartments>.Failure(nameResult.Error);

		return Result<TeachersDepartments>.Success(new TeachersDepartments(nameResult.Value));
	}

	public Result<Teacher> RegisterTeacher(
		string name,
		string surname,
		string? patronymic,
		string workingState,
		string jobTitle)
	{
		Result<TeacherName> nameCreation = TeacherName.Create(name, surname, patronymic);
		if (nameCreation.IsFailure)
			return Result<Teacher>.Failure(nameCreation.Error);

		Result<TeacherWorkingState> stateCreation = TeacherWorkingState.Create(workingState);
		if (stateCreation.IsFailure)
			return Result<Teacher>.Failure(stateCreation.Error);

		Result<TeacherJobTitle> jobTitleCreation = TeacherJobTitle.Create(jobTitle);
		if (jobTitleCreation.IsFailure)
			return Result<Teacher>.Failure(jobTitleCreation.Error);

		Teacher teacher = new Teacher(nameCreation.Value, stateCreation.Value, jobTitleCreation.Value, this);
		_teachers.Add(teacher);
		return Result<Teacher>.Success(teacher);
	}

	public Result<Teacher> FireTeacher(Teacher teacher)
	{
		if (_teachers.Any(t => t.Id == teacher.Id) == false)
			return Result<Teacher>.Failure(TeacherErrors.DoesntBelongDepartment(this));

		_teachers.Remove(teacher);
		return Result<Teacher>.Success(teacher);
	}

	public Result<Teacher> FindTeacher(
		string name,
		string surname,
		string? patronymic,
		string state,
		string jobTitle)
	{
		Teacher? teacher = _teachers.FirstOrDefault(t =>
			t.Name.Name == name &&
			t.Name.Surname == surname &&
			t.Name.Patronymic == patronymic &&
			t.State.State == state &&
			t.JobTitle.JobTitle == jobTitle);

		return teacher == null ?
			Result<Teacher>.Failure(TeacherErrors.NotFound()) :
			Result<Teacher>.Success(teacher);
	}

	public Result<TeachersDepartments> ChangeName(string name)
	{
		Result<DepartmentName> newName = DepartmentName.Create(name);
		if (newName.IsFailure)
			return Result<TeachersDepartments>.Failure(newName.Error);

		if (Name == newName.Value)
			return Result<TeachersDepartments>.Success(this);

		Name = newName.Value;
		return Result<TeachersDepartments>.Success(this);
	}
}
