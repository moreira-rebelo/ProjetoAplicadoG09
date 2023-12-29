using ISI.Domain.Entity;
using ISI.Domain.Repository;
using ISI.Domain.SeedWork.SearchableRepository;

namespace ISI.infrastructure.Repositories;

public class UserRepository: IUserRepository
{
    public Task Insert(User aggregate, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<User> Get(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task Delete(User aggregate, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task Update(User aggregate, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<SearchOutput<User>> Search(SearchInput input, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetUserByEmailAddress(string emailAddress, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}