using MediatR;

namespace ISI.Application.UseCases.Reservation.GetReservation;

public class GetReservationInput: IRequest<GetReservationOutput>
{
    public string ReservationCode { get; }

    public GetReservationInput(string reservationCode)
    {
        ReservationCode = reservationCode;

    }
    
}