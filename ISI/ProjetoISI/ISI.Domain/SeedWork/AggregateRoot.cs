namespace ISI.Domain.SeedWork;

public abstract class AggregateRoot<TId> : Entity<TId>
{
    protected AggregateRoot() : base() { }

    protected AggregateRoot(TId id) : base(id) { }
}
