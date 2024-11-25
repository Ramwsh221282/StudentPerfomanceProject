using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace SPerfomance.Application.Services.Authentication.Models;

public class Token
{
    public string UserId { get; init; }
    public bool IsExpired { get; init; }

    public Token(string token)
    {
        if (string.IsNullOrWhiteSpace(token))
        {
            IsExpired = true;
            UserId = string.Empty;
            return;
        }

        TokenValidationParameters parameters = BuildParameters();
        ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(
            token,
            parameters,
            out _
        );
        var userIdPrincipal = principal.FindFirst("userId");
        var lifeTimePrincipal = principal.FindFirst("exp");
        UserId = userIdPrincipal == null ? string.Empty : userIdPrincipal.Value;
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
