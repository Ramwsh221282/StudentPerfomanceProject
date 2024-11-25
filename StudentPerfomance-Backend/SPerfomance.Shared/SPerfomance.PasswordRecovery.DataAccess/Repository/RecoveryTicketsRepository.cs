using LiteDB.Async;
using SPerfomance.Application.PasswordRecoveryContext.Models;
using SPerfomance.Application.PasswordRecoveryContext.RepositoryAbstraction;

namespace SPerfomance.PasswordRecovery.DataAccess.Repository;

public sealed class RecoveryTicketsRepository : IPasswordRecoveryTicketsRepository
{
    private const string Connection = "Filename=password_recovery_database.db;";
    private const string Collection = "password_recovery_tickets";

    public async Task RegisterTicket(PasswordRecoveryTicket ticket, CancellationToken ct = default)
    {
        using var db = new LiteDatabaseAsync(Connection);
        var collection = db.GetCollection<PasswordRecoveryTicket>(Collection);
        await collection.InsertAsync(ticket);
    }

    public async Task DeleteTicket(PasswordRecoveryTicket ticket, CancellationToken ct = default)
    {
        using var db = new LiteDatabaseAsync(Connection);
        var collection = db.GetCollection<PasswordRecoveryTicket>(Collection);
        await collection.DeleteManyAsync(t => t.Email == ticket.Email || t.Token == ticket.Token);
    }

    public async Task<PasswordRecoveryTicket?> FindRecoveryTicket(
        string? token,
        CancellationToken ct = default
    )
    {
        using var db = new LiteDatabaseAsync(Connection);
        var collection = db.GetCollection<PasswordRecoveryTicket>(Collection);
        var ticket = await collection.FindOneAsync(t => t.Token == token);
        return ticket;
    }
}
