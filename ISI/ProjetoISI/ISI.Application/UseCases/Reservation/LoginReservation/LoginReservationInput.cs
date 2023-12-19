using MediatR;

namespace ISI.Application.UseCases.Reservation.LoginReservation;

public class LoginReservationInput: IRequest<LoginReservationOutput>
{
    public string ReservationCode { get; set; }
    public string ReservationPassword { get; set; }
    
    public LoginReservationInput(string reservationCode, string reservationPassword)
    {
        ReservationCode = reservationCode;
        ReservationPassword = reservationPassword;
    }
}