using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSessions;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSessions.Abstractions;
using SPerfomance.Domain.Models.StudentGroups;
using SPerfomance.Domain.Models.StudentGroups.Abstractions;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.PerfomanceContext.AssignmentSessions.Commands.Create;

public class CreateAssignmentSessionCommandHandler(
    IAssignmentSessionsRepository sessions,
    IStudentGroupsRepository groups
) : ICommandHandler<CreateAssignmentSessionCommand, AssignmentSession>
{
    private readonly IAssignmentSessionsRepository _sessions = sessions;

    private readonly IStudentGroupsRepository _groups = groups;

    public async Task<Result<AssignmentSession>> Handle(CreateAssignmentSessionCommand command)
    {
        IReadOnlyCollection<StudentGroup> groups = await _groups.GetAll();
        Result<AssignmentSession> session = AssignmentSession.Create(
            groups,
            command.StartDate,
            command.EndDate
        );
        if (session.IsFailure)
            return session;

        await _sessions.Insert(session.Value);
        return session;
    }
}
