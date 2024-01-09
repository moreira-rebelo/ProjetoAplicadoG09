using ISI.Domain.Functions;
using ISI.Domain.SeedWork;
using ISI.Domain.Validation;
using ISI.Domain.ValueObject;

namespace ISI.Domain.Entity;

public class Reservation : AggregateRoot<string>
{

    public Reservation(Guid userId, DateTime checkIn, DateTime checkOut, string roomNumber) : base(ReservationCodeGenerator.GenerateReservationCode())
    {
        UserId = userId;
        CheckIn = checkIn;
        CheckOut = checkOut;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
        ReservationPassword = ReservationCodeGenerator.GenerateReservationPassword();
        RoomNumber = roomNumber;
        Validate();
    }

    public Guid UserId { get; private set; }
    public DateTime CheckIn { get; private set; }
    public DateTime CheckOut { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public DateTime UpdatedAt { get; private set; }

    public string ReservationPassword { get; private set; }
    public string RoomNumber { get; private set; }


    public bool IsValid()
    {
        var now = DateTime.UtcNow;
        return CheckIn < CheckOut && CheckIn >= now && CheckOut > now;
    }

    private void Validate()
    {
        DomainValidation.NotNullOrEmpty(ReservationPassword, nameof(ReservationPassword));
        DomainValidation.MaxLength(ReservationPassword, 10, nameof(ReservationPassword));
        DomainValidation.NotNullOrEmpty(ReservationPassword, nameof(ReservationPassword));
        DomainValidation.MaxLength(ReservationPassword, 10, nameof(ReservationPassword));
        //DomainValidation.GreaterThanOrEqualDate(CheckIn, DateTime.UtcNow, nameof(CheckIn));
        //\\DomainValidation.LessThanOrEqualDate(Che1222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222ckOut, CheckIn, nameof(CheckOut));
    }
}