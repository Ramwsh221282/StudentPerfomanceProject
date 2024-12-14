using SPerfomance.Application.Abstractions;
using SPerfomance.Application.PerfomanceContext.AssignmentSessions.Commands.MakeAssignment;
using SPerfomance.Domain.Models.PerfomanceContext.Models.Assignments.Errors;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSession.Errors;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSessions.Abstractions;
using SPerfomance.Domain.Models.PerfomanceContext.Models.StudentAssignments;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Api.Features.PerfomanceContext.Contracts;

public class MakeAssignmentCommandHandler(
    IAssignmentSessionsRepository sessions,
    IStudentAssignmentsRepository assignments
) : ICommandHandler<MakeAssignmentCommand, StudentAssignment>
{
    public async Task<Result<StudentAssignment>> Handle(
        MakeAssignmentCommand command,
        CancellationToken ct = default
    )
    {
        var session = await sessions.GetActiveSession(ct);
        if (session == null)
            return AssignmentSessionErrors.NoActiveFound();

        var assignment = await assignments.ReceiveAssignment(Guid.Parse(command.Id), ct);

        if (assignment == null)
            return AssignmentErrors.NotFound();

        var result = assignment.Assign(command.Mark);
        if (result.IsFailure)
            return result;

        await assignments.UpdateAssignmentValue(result.Value, ct);
        return result;
    }
}
