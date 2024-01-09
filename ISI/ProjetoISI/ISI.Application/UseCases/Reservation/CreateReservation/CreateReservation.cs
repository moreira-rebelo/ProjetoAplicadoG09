using ISI.Application.Interfaces;
using ISI.Domain.Repository;
using ISI.Domain.Entity;
using ISI.Domain.ValueObject;

namespace ISI.Application.UseCases.Reservation.CreateReservation;

public class CreateReservation : ICreateReservation
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateReservation(IReservationRepository reservationRepository, IUnitOfWork unitOfWork,
        IUserRepository userRepository)
    {
        _reservationRepository = reservationRepository;
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
    }

    public async Task<CreateReservationOutput> Handle(CreateReservationInput request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByEmailAddress(request.Email, cancellationToken);

        if (user == null)
        {
            user = new Domain.Entity.User(request.FirstName, request.LastName, new Email(request.Email));
            await _userRepository.Insert(user, cancellationToken);
        }
        else
        {
            user.Update(request.FirstName, request.LastName);
            await _userRepository.Update(user, cancellationToken);
        }

        var room = await _reservationRepository.GetRoom(cancellationToken);
        var reservation = new Domain.Entity.Reservation(user.Id, request.CheckIn, request.CheckOut, room.Id);

        await _reservationRepository.Insert(reservation, cancellationToken);

        await _unitOfWork.Commit(cancellationToken);

        return CreateReservationOutput.FromDomain(reservation);
    }
}