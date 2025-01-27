using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSession;

namespace SPerfomance.Application.PerfomanceContext.AssignmentSessions.Commands.CloseAssignmentSession;

public class CloseAssignmentSessionCommand(Guid id) : ICommand<AssignmentSession>
{
    public Guid Id { get; init; } = id;

    public CloseAssignmentSessionCommand(string id)
        : this(Guid.Parse(id)) { }
}
