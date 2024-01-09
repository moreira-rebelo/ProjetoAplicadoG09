using ISI.Domain.Repository;

namespace ISI.Application.UseCases.Reservation.LoginReservation;

public class LoginReservation : ILoginReservation
{
    private readonly IReservationRepository _reservationRepository;

    public LoginReservation(IReservationRepository reservationRepository)
    {
        _reservationRepository = reservationRepository;
    }

    public async Task<LoginReservationOutput> Handle(LoginReservationInput input, CancellationToken cancellationToken)
    {
        await _reservationRepository.ReservationLogin(
            input.ReservationCode,
            input.ReservationPassword,
            cancellationToken
        );

        var token = JwtTokenGenerator.GenerateToken(input.ReservationCode);

        return new LoginReservationOutput(token);
    }
}