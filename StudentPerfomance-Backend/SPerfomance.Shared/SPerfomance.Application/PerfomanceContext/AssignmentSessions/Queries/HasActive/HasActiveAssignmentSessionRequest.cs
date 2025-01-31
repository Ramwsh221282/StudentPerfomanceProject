using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSession.Abstractions;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.PerfomanceContext.AssignmentSessions.Queries.HasActive;

public sealed record HasActiveAssignmentSessionResponse(bool Has);

public sealed record HasActiveAssignmentSessionRequest : IQuery<HasActiveAssignmentSessionResponse>;

public sealed class HasActiveAssignmentSessionRequestHandler
    : IQueryHandler<HasActiveAssignmentSessionRequest, HasActiveAssignmentSessionResponse>
{
    private readonly IAssignmentSessionsRepository _repository;

    public HasActiveAssignmentSessionRequestHandler(IAssignmentSessionsRepository repository) =>
        _repository = repository;

    public async Task<Result<HasActiveAssignmentSessionResponse>> Handle(
        HasActiveAssignmentSessionRequest command,
        CancellationToken ct = default
    )
    {
        bool result = await _repository.HasAnyActive(ct);
        return new HasActiveAssignmentSessionResponse(result);
    }
}
