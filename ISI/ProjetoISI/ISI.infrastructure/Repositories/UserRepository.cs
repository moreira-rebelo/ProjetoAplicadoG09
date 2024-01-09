using ISI.Application.Exceptions;
using ISI.Domain.Entity;
using ISI.Domain.Repository;
using ISI.Domain.SeedWork.SearchableRepository;
using Microsoft.EntityFrameworkCore;

namespace ISI.infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IsiDbContext _context;

    private DbSet<User> _users
        => _context.Set<User>();

    public UserRepository(IsiDbContext context)
    {
        _context = context;
    }

    public async Task Insert(User aggregate, CancellationToken cancellationToken)
    {
        await _users.AddAsync(aggregate, cancellationToken);
    }

    public async Task<User> Get(Guid id, CancellationToken cancellationToken)
    {
        var user = await _users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        NotFoundException.ThrowIfNull(user, $"Reservation '{id}' not found.");

        return user!;
    }

    public Task Delete(User aggregate, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task Update(User aggregate, CancellationToken cancellationToken)
    {
        _users.Update(aggregate);
        await _context.SaveChangesAsync(cancellationToken);
    }
    
    public Task<SearchOutput<User, Guid>> Search(SearchInput input, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<User> GetUserByEmailAddress(string emailAddress, CancellationToken cancellationToken)
    {
        var user = await _users
            .FromSqlRaw("SELECT * FROM public.\"user\" WHERE email = {0}", emailAddress)
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);
        
        return user; 
        
    }
}