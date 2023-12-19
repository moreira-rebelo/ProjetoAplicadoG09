namespace ISI.Application.UseCases.Reservation.LoginReservation;

public class LoginReservationOutput
{
    public string Token { get; set; }
    
    public LoginReservationOutput(string token)
    {
        Token = token;
    }
}