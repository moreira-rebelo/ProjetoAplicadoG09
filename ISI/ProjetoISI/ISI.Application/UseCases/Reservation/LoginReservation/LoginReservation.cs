using ISI.Application.Interfaces;
using ISI.Domain.Repository;

namespace ISI.Application.UseCases.Reservation.LoginReservation;

public class LoginReservation: ILoginReservation
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public LoginReservation(IReservationRepository reservationRepository, IUnitOfWork unitOfWork)
    {
        _reservationRepository = reservationRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<LoginReservationOutput> Handle(LoginReservationInput input, CancellationToken cancellationToken)
    {
         await _reservationRepository.ReservationLogin(
            input.ReservationCode,
            input.ReservationPassword,
            cancellationToken
        );
        
         await _unitOfWork.Commit(cancellationToken);
        
        var token = JwtTokenGenerator.GenerateToken(input.ReservationCode);
        
        return new LoginReservationOutput(token);
    }
}