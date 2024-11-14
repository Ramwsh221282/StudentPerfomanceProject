using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.Teachers;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.Departments.Commands.UpdateTeacher;

public class UpdateTeacherCommand(
    Teacher? teacher,
    string? newName,
    string? newSurname,
    string? NewPatronymic,
    string? newJobTitle,
    string? newState
) : ICommand<Teacher>
{
    public Teacher? Teacher { get; init; } = teacher;

    public string NewName { get; init; } = newName.ValueOrEmpty();

    public string NewSurname { get; init; } = newSurname.ValueOrEmpty();

    public string NewPatronymic { get; init; } = NewPatronymic.ValueOrEmpty();

    public string NewJobTitle { get; init; } = newJobTitle.ValueOrEmpty();

    public string NewState { get; init; } = newState.ValueOrEmpty();
}
