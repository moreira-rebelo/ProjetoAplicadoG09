using ISI.Application.Exceptions;
using ISI.Domain.Entity;
using ISI.Domain.Repository;
using ISI.Domain.SeedWork.SearchableRepository;
using ISI.Domain.ValueObject;
using Microsoft.EntityFrameworkCore;

namespace ISI.infrastructure.Repositories;

public class ReservationRepository : IReservationRepository
{
    private readonly IsiDbContext _context;

    public ReservationRepository(IsiDbContext context)
    {
        _context = context;
    }

    private DbSet<Reservation> _reservations
        => _context.Set<Reservation>();

    private DbSet<Entry> _entries
        => _context.Set<Entry>();

    private DbSet<Room> _rooms
        => _context.Set<Room>();

    public async Task Insert(Reservation aggregate, CancellationToken cancellationToken)
    {
        await _reservations.AddAsync(aggregate, cancellationToken);
    }

    public async Task<Reservation> Get(string reservationCode, CancellationToken cancellationToken)
    {
        var reservation = await _reservations
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == reservationCode, cancellationToken);

        NotFoundException.ThrowIfNull(reservation, $"Reservation '{reservationCode}' not found.");

        return reservation!;
    }

    public Task Delete(Reservation aggregate, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<Room> GetRoom(CancellationToken cancellationToken)
    {
        var room = await _rooms
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);
        
        return room!;
    }

    public Task Update(Reservation aggregate, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<SearchOutput<Reservation, string>> Search(SearchInput input, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task ReservationLogin(string reservationCode, string reservationPassword,
        CancellationToken cancellationToken)
    {
        var reservation = await _reservations
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == reservationCode, cancellationToken);

        NotFoundException.ThrowIfNull(reservation, $"Reservation '{reservationCode}' not found.");

        if (reservation!.ReservationPassword != reservationPassword)
            throw new AuthenticationFailedException("Invalid password.");
    }

    public async Task CreateEntry(Entry entry, CancellationToken cancellationToken)
    {
        await _entries.AddAsync(entry, cancellationToken);
    }
}