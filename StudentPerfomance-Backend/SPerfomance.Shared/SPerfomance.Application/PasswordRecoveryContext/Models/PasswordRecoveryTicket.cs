namespace SPerfomance.Application.PasswordRecoveryContext.Models;

public sealed class PasswordRecoveryTicket
{
    public string Token { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;
}
