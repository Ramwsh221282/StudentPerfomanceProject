using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSessions;

namespace SPerfomance.Application.PerfomanceContext.AssignmentSessions.Commands.Create;

public class CreateAssignmentSessionCommand(DateTime startDate, DateTime endDate)
    : ICommand<AssignmentSession>
{
    public DateTime StartDate { get; init; } = startDate;

    public DateTime EndDate { get; init; } = endDate;
}
