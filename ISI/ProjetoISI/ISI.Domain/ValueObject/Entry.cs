using ISI.Domain.Entity;
using ISI.Domain.Validation;

namespace ISI.Domain.ValueObject;

public class Entry
{
    public DateTime AccessTime { get; }
    public string RoomNumber { get; }
    public string ReservationCode { get; }

    public Entry(string roomNumber, string reservationCode)
    {
        RoomNumber = roomNumber;
        ReservationCode = reservationCode;
        AccessTime = DateTime.Now;
    }

    private void Validate()
    {
        DomainValidation.NotNullOrEmpty(RoomNumber, nameof(RoomNumber));
        DomainValidation.NotNullOrEmpty(ReservationCode, nameof(ReservationCode));
    }
}

