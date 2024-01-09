namespace ISI.Domain.SeedWork;

public interface IGenericRepository<TAggregate, TId> : IRepository
    where TAggregate : AggregateRoot<TId>
{
    Task Insert(TAggregate aggregate, CancellationToken cancellationToken);
    Task<TAggregate> Get(TId id, CancellationToken cancellationToken);
    Task Delete(TAggregate aggregate, CancellationToken cancellationToken);
    Task Update(TAggregate aggregate, CancellationToken cancellationToken);
}
