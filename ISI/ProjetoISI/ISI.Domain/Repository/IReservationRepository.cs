using ISI.Domain.Entity;
using ISI.Domain.SeedWork;
using ISI.Domain.SeedWork.SearchableRepository;

namespace ISI.Domain.Repository;

public interface IReservationRepository: IGenericRepository<Reservation> , ISearchableRepository<Reservation>
{
    public Task<Reservation> ReservationLogin(
        String reservationCode,
        String reservationPassword,
        CancellationToken cancellationToken
    );
}