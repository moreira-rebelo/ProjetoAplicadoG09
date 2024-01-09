using ISI.Domain.Entity;
using ISI.Domain.SeedWork;
using ISI.Domain.SeedWork.SearchableRepository;

namespace ISI.Domain.Repository;

public interface IUserRepository : IGenericRepository<User, Guid>, ISearchableRepository<User, Guid>
{
    Task<User?> GetUserByEmailAddress(string emailAddress, CancellationToken cancellationToken);
}
