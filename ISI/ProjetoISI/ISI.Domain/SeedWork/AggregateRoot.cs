namespace ISI.Domain.SeedWork;

public abstract class AggregateRoot : Entity
{
    protected AggregateRoot() : base() { }

    protected AggregateRoot(Guid id) : base(id) { } // Additional constructor
}
