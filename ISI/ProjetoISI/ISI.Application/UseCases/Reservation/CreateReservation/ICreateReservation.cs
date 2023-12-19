using MediatR;

namespace ISI.Application.UseCases.Reservation.CreateReservation;

public interface ICreateReservation: IRequestHandler<CreateReservationInput, CreateReservationOutput>
{
    
}