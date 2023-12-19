using ISI.Domain.Entity;
using ISI.Domain.SeedWork;
using ISI.Domain.SeedWork.SearchableRepository;

namespace ISI.Domain.Repository;

public interface IReservationRepository: IGenericRepository<Reservation> , ISearchableRepository<Reservation>
{
    public Task ReservationLogin(
        String reservationCode,
        String reservationPassword,
        CancellationToken cancellationToken
    );
}