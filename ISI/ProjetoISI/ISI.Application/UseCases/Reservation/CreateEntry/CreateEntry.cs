using ISI.Application.Interfaces;
using ISI.Domain.Repository;
using ISI.Domain.ValueObject;

namespace ISI.Application.UseCases.Reservation.CreateEntry;

public class CreateEntry : ICreateEntry
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IUnitOfWork _unitOfWork;


    public CreateEntry(IReservationRepository reservationRepository, IUnitOfWork unitOfWork)
    {
        _reservationRepository = reservationRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CreateEntryOutput> Handle(CreateEntryInput input, CancellationToken cancellationToken)
    {
        var entry = new Entry(input.RoomNumber, input.ReservationCode);

        await _reservationRepository.CreateEntry(entry, cancellationToken);

        await _unitOfWork.Commit(cancellationToken);


        return new CreateEntryOutput(entry.AccessTime);
    }
}