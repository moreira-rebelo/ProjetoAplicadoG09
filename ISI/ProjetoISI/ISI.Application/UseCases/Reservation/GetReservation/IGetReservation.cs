using MediatR;

namespace ISI.Application.UseCases.Reservation.GetReservation;

public interface IGetReservation: IRequestHandler<GetReservationInput, GetReservationOutput>
{
    
}