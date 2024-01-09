namespace ISI.Domain.SeedWork.SearchableRepository;

public interface ISearchableRepository<TAggregate, TId>
    where TAggregate : AggregateRoot<TId>
{
    Task<SearchOutput<TAggregate, TId>> Search(
        SearchInput input,
        CancellationToken cancellationToken
    );
}
