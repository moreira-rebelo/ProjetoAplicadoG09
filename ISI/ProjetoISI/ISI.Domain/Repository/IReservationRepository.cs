using ISI.Domain.Entity;
using ISI.Domain.SeedWork;
using ISI.Domain.SeedWork.SearchableRepository;
using ISI.Domain.ValueObject;

namespace ISI.Domain.Repository;

public interface IReservationRepository : IGenericRepository<Reservation, string>, ISearchableRepository<Reservation, string>
{
    Task ReservationLogin(
        string reservationCode,
        string reservationPassword,
        CancellationToken cancellationToken
    );

    Task CreateEntry(Entry entry, CancellationToken cancellationToken);
    Task <Room> GetRoom(CancellationToken cancellationToken);
}
