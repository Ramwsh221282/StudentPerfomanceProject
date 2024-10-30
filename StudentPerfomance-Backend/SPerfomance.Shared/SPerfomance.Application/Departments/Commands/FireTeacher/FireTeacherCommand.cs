using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.Teachers;

namespace SPerfomance.Application.Departments.Commands.FireTeacher;

public class FireTeacherCommand(Teacher? teacher) : ICommand<Teacher>
{
	public Teacher? Teacher { get; init; } = teacher;
}
