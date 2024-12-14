using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSession;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSession.Errors;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSession.ValueObject;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSessions.Abstractions;
using SPerfomance.Domain.Models.StudentGroups.Abstractions;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.PerfomanceContext.AssignmentSessions.Commands.Create;

public class CreateAssignmentSessionCommandHandler(
    IAssignmentSessionsRepository sessions,
    IStudentGroupsRepository groups
) : ICommandHandler<CreateAssignmentSessionCommand, AssignmentSession>
{
    public async Task<Result<AssignmentSession>> Handle(
        CreateAssignmentSessionCommand command,
        CancellationToken ct = default
    )
    {
        if (string.IsNullOrWhiteSpace(command.Type))
            return AssignmentSessionErrors.AssignmentSessionSemesterTypeEmpty();

        if (command.Number == null)
            return AssignmentSessionErrors.AssignmentSessionSemesterNumberEmpty();

        var type = AssignmentSessionSemesterType.Create(command.Type);
        if (type.IsFailure)
            return type.Error;

        var number = AssignmentSessionNumber.Create(command.Number.Value);
        if (number.IsFailure)
            return number.Error;

        var allGroups = await groups.GetAll(ct);
        var session = AssignmentSession.Create(
            allGroups,
            command.StartDate,
            number.Value,
            type.Value
        );

        if (session.IsFailure)
            return session;

        await sessions.Insert(session.Value, ct);
        return session;
    }
}
