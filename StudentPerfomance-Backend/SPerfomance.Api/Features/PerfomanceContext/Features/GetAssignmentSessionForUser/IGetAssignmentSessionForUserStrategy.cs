using Microsoft.AspNetCore.Http.HttpResults;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSessions;

namespace SPerfomance.Api.Features.PerfomanceContext.Features.GetAssignmentSessionForUser;

public interface IGetAssignmentSessionForUserStrategy
{
    Task<
        Results<
            UnauthorizedHttpResult,
            NotFound<string>,
            BadRequest<string>,
            Ok<TeacherAssignmentSession>
        >
    > GetAssignmentSessions();
}
