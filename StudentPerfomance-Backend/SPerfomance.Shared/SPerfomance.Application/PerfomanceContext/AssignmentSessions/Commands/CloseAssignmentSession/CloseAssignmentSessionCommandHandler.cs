using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSessions;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSessions.Abstractions;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSessions.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.PerfomanceContext.AssignmentSessions.Commands.CloseAssignmentSession;

public class CloseAssignmentSessionCommandHandler(IAssignmentSessionsRepository repository)
    : ICommandHandler<CloseAssignmentSessionCommand, AssignmentSession>
{
    private readonly IAssignmentSessionsRepository _repository = repository;

    public async Task<Result<AssignmentSession>> Handle(CloseAssignmentSessionCommand command)
    {
        AssignmentSession? requested = await _repository.GetById(command.Id);
        if (requested == null)
            return AssignmentSessionErrors.CantFindById(command.Id);

        Result<AssignmentSession> closed = requested.CloseSession();
        if (closed.IsFailure)
            return closed;

        await _repository.Update(closed);
        return closed;
    }
}
