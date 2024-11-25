using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSessions;
using SPerfomance.Domain.Models.StudentGroups.Abstractions;

namespace SPerfomance.Application.PerfomanceContext.AssignmentSessions.Commands.CloseAssignmentSession;

public class AssignmentSessionStudentGroupActiveSemesterResolver(
    IStudentGroupsRepository groupsRepository,
    AssignmentSession session
)
{
    public async Task ResolveGroupsActiveSemesters(CancellationToken ct)
    {
        var weeks = session.Weeks.ToArray();
        var groups = weeks.Select(w => w.Group).ToArray();
        foreach (var group in groups)
        {
            group.SetNextSemester();
            await groupsRepository.SetNextSemester(group, ct);
        }
    }
}
