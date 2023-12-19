using ISI.Application.Interfaces;
using ISI.Application.UseCases.Reservation.LoginReservation;
using ISI.Domain.Repository;

namespace ISI.Application.UseCases.User.GetUser;

public class GetUser
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public GetUser(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<GetUserOutput> Handle(GetUserInput input, CancellationToken cancellationToken)
    {
        var user  = await _userRepository.GetUserByEmailAddress(input.Email, cancellationToken);
        
        return GetUserOutput.FromUser(user);
    }
}