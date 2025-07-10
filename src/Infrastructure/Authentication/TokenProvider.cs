using System.Security.Claims;
using System.Text;
using Application.Abstractions.Authentication;
using Domain.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Authentication;

internal class TokenProvider(IConfiguration configuration) : ITokenProvider
{
    public string CreateFor(User user)
    {
        var jwt = configuration.GetSection(JwtDefaults.SectionName);
        var secret = jwt.GetValue<string>(JwtDefaults.SecretSection)!;

        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(
                [
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString())
                ]
            ),
            Expires = DateTime.UtcNow.AddMinutes(jwt.GetValue<int>(JwtDefaults.ExpirationInMinutesSection)),
            SigningCredentials = credentials,
            Issuer = jwt.GetValue<string>(JwtDefaults.ValidIssuerSection),
            Audience = "developer"
        };

        var handler = new JsonWebTokenHandler();
        return handler.CreateToken(tokenDescriptor);
    }
}