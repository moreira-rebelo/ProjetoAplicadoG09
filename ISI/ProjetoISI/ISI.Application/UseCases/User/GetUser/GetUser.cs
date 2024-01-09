using ISI.Application.Interfaces;
using ISI.Application.UseCases.Reservation.LoginReservation;
using ISI.Domain.Repository;

namespace ISI.Application.UseCases.User.GetUser;

public class GetUser: IGetUser
{
    private readonly IUserRepository _userRepository;
    private readonly IReservationRepository _reservationRepository;
    
    public GetUser(IUserRepository userRepository, IReservationRepository reservationRepository)
    {
        _userRepository = userRepository;
        _reservationRepository = reservationRepository;
    }

    public async Task<GetUserOutput> Handle(GetUserInput input, CancellationToken cancellationToken)
    {
        
        var reservation  = await _reservationRepository.Get(input.ReservationCode, cancellationToken);
        
        var user  = await _userRepository.Get(reservation.UserId, cancellationToken);
        
        return GetUserOutput.FromUser(user);
    }
}