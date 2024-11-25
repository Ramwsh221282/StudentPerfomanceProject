using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using SPerfomance.Application.Services.Authentication.Abstractions;
using SPerfomance.Domain.Models.Users;

namespace SPerfomance.Application.Services.Authentication;

public sealed class JwtTokenService : IJwtTokenService
{
    public const string _secret = "a2c744df-4775-410c-af5a-975a377dab1b";
    private const string _recoverySecret = "716ec6cb-e13b-47ac-8809-131de39b3791";

    public string GenerateToken(User user)
    {
        Claim[] claims = [new("userId", user.Id.ToString())];

        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret)),
            SecurityAlgorithms.HmacSha256
        );

        var token = new JwtSecurityToken(
            claims: claims,
            signingCredentials: credentials,
            expires: DateTime.UtcNow.AddHours(6)
        );

        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
        return tokenValue;
    }

    public string GenerateRecoveryTicket(User user)
    {
        Claim[] claims = [new("userId", user.Id.ToString()), new("userEmail", user.Email.Email)];
        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_recoverySecret)),
            SecurityAlgorithms.HmacSha256
        );

        var token = new JwtSecurityToken(
            claims: claims,
            signingCredentials: credentials,
            expires: (DateTime.UtcNow.AddHours(1))
        );

        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
        return tokenValue;
    }
}
