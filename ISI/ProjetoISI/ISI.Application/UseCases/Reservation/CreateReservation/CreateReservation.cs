using ISI.Application.Interfaces;
using ISI.Domain.Repository;
using ISI.Domain.ValueObject;

namespace ISI.Application.UseCases.Reservation.CreateReservation;

public class CreateReservation : ICreateReservation
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateReservation(IReservationRepository reservationRepository, IUnitOfWork unitOfWork, IUserRepository userRepository)
    {
        _reservationRepository = reservationRepository;
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
    }
    
    public async Task<CreateReservationOutput> Handle(CreateReservationInput request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.Get(request.UserId, cancellationToken);
        var reservation = new Reservation(user, request.StartDate, request.EndDate);
        await _reservationRepository.Create(reservation, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);
        return new CreateReservationOutput(reservation.Id);
    }

    
}