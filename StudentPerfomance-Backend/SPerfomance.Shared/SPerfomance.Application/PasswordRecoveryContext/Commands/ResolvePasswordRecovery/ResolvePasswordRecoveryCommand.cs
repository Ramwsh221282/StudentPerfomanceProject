using SPerfomance.Application.Abstractions;
using SPerfomance.Application.PasswordRecoveryContext.Models;

namespace SPerfomance.Application.PasswordRecoveryContext.Commands.ResolvePasswordRecovery;

public sealed class ResolvePasswordRecoveryCommand(string token) : ICommand<PasswordRecoveryTicket>
{
    public string? Token { get; set; } = token;
}
