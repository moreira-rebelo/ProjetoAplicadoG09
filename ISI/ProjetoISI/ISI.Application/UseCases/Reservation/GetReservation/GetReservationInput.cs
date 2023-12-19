using MediatR;

namespace ISI.Application.UseCases.Reservation.GetReservation;

public class GetReservationInput: IRequest<GetReservationOutput>
{
    public Guid ReservationCode { get; }

    public GetReservationInput(Guid reservationCode)
    {
        ReservationCode = reservationCode;

    }
    
}