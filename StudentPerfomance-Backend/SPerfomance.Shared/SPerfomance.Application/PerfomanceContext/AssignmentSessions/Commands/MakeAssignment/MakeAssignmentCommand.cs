using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.PerfomanceContext.Models.StudentAssignments;

namespace SPerfomance.Application.PerfomanceContext.AssignmentSessions.Commands.MakeAssignment;

public class MakeAssignmentCommand(string id, int mark) : ICommand<StudentAssignment>
{
    public string Id { get; init; } = id;

    public int Mark { get; init; } = mark;
}
