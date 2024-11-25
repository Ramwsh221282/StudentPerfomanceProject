using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace SPerfomance.Application.Services.Authentication.Models;

public sealed class PasswordRecoveryToken : Token
{
    public string RecoveryUserId { get; private set; }
    public string RecoveryUserEmail { get; private set; }
    public bool IsRecoveryExpired { get; private set; } = true;

    public PasswordRecoveryToken(string token)
        : base(token)
    {
        if (string.IsNullOrWhiteSpace(token))
        {
            IsExpired = true;
            RecoveryUserId = string.Empty;
            RecoveryUserEmail = string.Empty;
        }

        TokenValidationParameters parameters = BuildParameters();
        ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(
            token,
            parameters,
            out _
        );
        var userIdPrincipal = principal.FindFirst("userId");
        var userEmailPrincipal = principal.FindFirst("userEmail");
        var lifeTimePrincipal = principal.FindFirst("exp");
        RecoveryUserId = userIdPrincipal == null ? string.Empty : userIdPrincipal.Value;
        RecoveryUserEmail = userEmailPrincipal == null ? string.Empty : userEmailPrincipal.Value;
        long time = lifeTimePrincipal == null ? 0 : long.Parse(lifeTimePrincipal.Value);
        IsExpired = VerifyLifeTime(time);
    }

    private TokenValidationParameters BuildParameters()
    {
        SymmetricSecurityKey securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(JwtTokenService._secret)
        );
        TokenValidationParameters parameter = new TokenValidationParameters()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = false,
            ValidateLifetime = false,
            IssuerSigningKeys = [securityKey],
        };
        return parameter;
    }

    private bool VerifyLifeTime(long time)
    {
        if (time == 0)
            return true;

        DateTime tokenDate = DateTimeOffset.FromUnixTimeSeconds(time).UtcDateTime;
        DateTime now = DateTime.Now.ToUniversalTime();
        return tokenDate <= now;
    }
}
