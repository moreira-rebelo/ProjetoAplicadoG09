using ISI.Domain.Entity;
using ISI.Domain.Repository;
using ISI.Domain.SeedWork.SearchableRepository;

namespace ISI.infrastructure.Repositories;

public class ReservationRepository: IReservationRepository
{
    public Task Insert(Reservation aggregate, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Reservation> Get(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task Delete(Reservation aggregate, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task Update(Reservation aggregate, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<SearchOutput<Reservation>> Search(SearchInput input, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Reservation> ReservationLogin(string reservationCode, string reservationPassword, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}