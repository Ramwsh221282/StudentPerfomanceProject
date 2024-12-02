using SPerfomance.Application.Abstractions;
using SPerfomance.Application.PerfomanceContext.AssignmentSessions.Abstractions;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSessions;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSessions.Abstractions;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSessions.Errors;
using SPerfomance.Domain.Models.StudentGroups.Abstractions;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.PerfomanceContext.AssignmentSessions.Commands.CloseAssignmentSession;

public class CloseAssignmentSessionCommandHandler(
    IAssignmentSessionsRepository repository,
    IStudentGroupsRepository groups,
    IControlWeekGroupDocument document
) : ICommandHandler<CloseAssignmentSessionCommand, AssignmentSession>
{
    public async Task<Result<AssignmentSession>> Handle(
        CloseAssignmentSessionCommand command,
        CancellationToken ct = default
    )
    {
        var requested = await repository.GetById(command.Id, ct);
        if (requested == null)
            return AssignmentSessionErrors.CantFindById(command.Id);

        var closed = requested.CloseSession();
        if (closed.IsFailure)
            return closed;

        AssignmentSessionStudentGroupActiveSemesterResolver resolver =
            new AssignmentSessionStudentGroupActiveSemesterResolver(groups, document, closed);
        await resolver.ResolveGroupsActiveSemesters(ct);
        await repository.Remove(closed, ct);
        return closed;
    }
}
