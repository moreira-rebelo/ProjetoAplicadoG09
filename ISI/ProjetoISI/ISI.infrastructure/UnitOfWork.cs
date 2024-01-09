using ISI.Application.Interfaces;

namespace ISI.infrastructure;

public class UnitOfWork
    : IUnitOfWork
{
    private readonly IsiDbContext _context;

    public UnitOfWork(IsiDbContext context)
        => _context = context;

    public Task Commit(CancellationToken cancellationToken)
        => _context.SaveChangesAsync(cancellationToken);

    public Task Rollback(CancellationToken cancellationToken)
        => Task.CompletedTask;
}