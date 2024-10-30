using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.TeacherDepartments;
using SPerfomance.Domain.Models.Teachers;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.Departments.Queries.GetTeacherFromDepartment;

public class GetTeacherFromDepartmentQuery
(
	TeachersDepartments? department,
	string? name,
	string? surname,
	string? patronymic,
	string? jobTitle,
	string? workingState
)
 : IQuery<Teacher>
{
	public TeachersDepartments? Department { get; init; } = department;

	public string Name { get; init; } = name.ValueOrEmpty();

	public string Surname { get; init; } = surname.ValueOrEmpty();

	public string Patronymic { get; init; } = patronymic.ValueOrEmpty();

	public string JobTitle { get; init; } = jobTitle.ValueOrEmpty();

	public string WorkingState { get; init; } = workingState.ValueOrEmpty();
}
