using ISI.Application.Interfaces;
using ISI.Domain.Repository;

namespace ISI.Application.UseCases.Reservation.GetReservation;

public class GetReservation : IGetReservation
{

    private readonly IReservationRepository _reservationRepository;

    public GetReservation(IReservationRepository reservationRepository)
    {
        _reservationRepository = reservationRepository;
    }

    public async Task<GetReservationOutput> Handle(GetReservationInput input, CancellationToken cancellationToken)
    {
        var reservation = await _reservationRepository.Get(input.ReservationCode, cancellationToken);

        return GetReservationOutput.FromReservation(reservation);

    }
}