using SPerfomance.Application.PasswordRecoveryContext.Models;

namespace SPerfomance.Application.PasswordRecoveryContext.RepositoryAbstraction;

public interface IPasswordRecoveryTicketsRepository
{
    Task RegisterTicket(PasswordRecoveryTicket ticket, CancellationToken ct = default);

    Task DeleteTicket(PasswordRecoveryTicket ticket, CancellationToken ct = default);

    Task<PasswordRecoveryTicket?> FindRecoveryTicket(string? token, CancellationToken ct = default);
}
