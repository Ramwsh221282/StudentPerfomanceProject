namespace SPerfomance.Application.Users.Commands.RequestUserPasswordReset;

public interface IUserPasswordResetContainer
{
    public void ResolveTicket(Guid ticketId);

    public void RegisterTicket(string email, Guid ticketId);
}
