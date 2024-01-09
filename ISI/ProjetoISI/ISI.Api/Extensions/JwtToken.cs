using System.IdentityModel.Tokens.Jwt;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace ISI.Api.Extensions;

public class JwtToken : IJwtToken
{
    public string GetReservationFromToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

        var reservationClaim = securityToken?.Claims
            .FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;

        return reservationClaim;
    }
}