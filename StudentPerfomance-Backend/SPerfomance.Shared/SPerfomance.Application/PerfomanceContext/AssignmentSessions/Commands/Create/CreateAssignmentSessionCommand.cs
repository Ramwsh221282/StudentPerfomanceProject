using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSession;

namespace SPerfomance.Application.PerfomanceContext.AssignmentSessions.Commands.Create;

public class CreateAssignmentSessionCommand(DateTime startDate, string? type, byte? number)
    : ICommand<AssignmentSession>
{
    public DateTime StartDate { get; init; } = startDate;

    public string? Type { get; init; } = type;

    public byte? Number { get; init; } = number;
}
