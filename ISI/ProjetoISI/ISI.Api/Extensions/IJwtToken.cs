namespace ISI.Api.Extensions;

public interface IJwtToken
{
    string GetReservationFromToken(string token);

}