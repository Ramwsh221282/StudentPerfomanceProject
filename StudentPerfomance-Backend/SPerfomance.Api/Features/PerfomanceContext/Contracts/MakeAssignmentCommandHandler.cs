using SPerfomance.Application.Abstractions;
using SPerfomance.Application.PerfomanceContext.AssignmentSessions.Commands.MakeAssignment;
using SPerfomance.Domain.Models.PerfomanceContext.Models.Assignments.Errors;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSessions;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSessions.Abstractions;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSessions.Errors;
using SPerfomance.Domain.Models.PerfomanceContext.Models.StudentAssignments;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Api.Features.PerfomanceContext.Contracts;

public class MakeAssignmentCommandHandler(
    IAssignmentSessionsRepository sessions,
    IStudentAssignmentsRepository assignments
) : ICommandHandler<MakeAssignmentCommand, StudentAssignment>
{
    private readonly IAssignmentSessionsRepository _sessions = sessions;

    private readonly IStudentAssignmentsRepository _assignments = assignments;

    public async Task<Result<StudentAssignment>> Handle(MakeAssignmentCommand command)
    {
        AssignmentSession? session = await _sessions.GetActiveSession();
        if (session == null)
            return AssignmentSessionErrors.NoActiveFound();

        StudentAssignment? assignment = await _assignments.ReceiveAssignment(
            Guid.Parse(command.Id)
        );

        if (assignment == null)
            return AssignmentErrors.NotFound();

        Result<StudentAssignment> result = assignment.Assign(command.Mark);
        if (result.IsFailure)
            return result;

        await _assignments.UpdateAssignmentValue(result.Value);
        return result;
    }
}
