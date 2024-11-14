using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.Students;

namespace SPerfomance.Application.StudentGroups.Commands.RemoveStudent;

public class RemoveStudentCommand(Student? student) : ICommand<Student>
{
    public Student? Student { get; init; } = student;
}
