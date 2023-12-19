using ISI.Domain.Entity;
using ISI.Domain.Validation;

namespace ISI.Domain.ValueObject;

public class Entry
{
    public DateTime AccessTime { get; }
    public Guid RoomId { get; }
    public Guid ReservationId { get; }

    public Entry(Room roomId, Reservation reservationId)
    {
        RoomId = roomId.Id;
        ReservationId = reservationId.Id;
        AccessTime = DateTime.Now;
    }

    private void Validate()
    {
        DomainValidation.NotNullOrEmpty(RoomId.ToString(), nameof(RoomId));
        DomainValidation.NotNullOrEmpty(ReservationId.ToString(), nameof(ReservationId));
    }
}

