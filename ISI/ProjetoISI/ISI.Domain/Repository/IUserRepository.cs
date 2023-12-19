using ISI.Domain.Entity;
using ISI.Domain.SeedWork;
using ISI.Domain.SeedWork.SearchableRepository;

namespace ISI.Domain.Repository;

public interface IUserRepository: IGenericRepository<User> , ISearchableRepository<User> {
    Task<User> GetUserByEmailAddress(string emailAddress, CancellationToken cancellationToken);
}