using MediatR;

namespace ISI.Application.UseCases.Reservation.LoginReservation;

public interface ILoginReservation: IRequestHandler<LoginReservationInput, LoginReservationOutput>
{
    
}