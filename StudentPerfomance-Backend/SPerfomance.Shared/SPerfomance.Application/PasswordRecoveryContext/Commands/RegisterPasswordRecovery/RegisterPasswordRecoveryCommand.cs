using SPerfomance.Application.Abstractions;
using SPerfomance.Application.PasswordRecoveryContext.Models;

namespace SPerfomance.Application.PasswordRecoveryContext.Commands.RegisterPasswordRecovery;

public class RegisterPasswordRecoveryCommand : ICommand<PasswordRecoveryTicket>
{
    public string? Email { get; set; }
}
