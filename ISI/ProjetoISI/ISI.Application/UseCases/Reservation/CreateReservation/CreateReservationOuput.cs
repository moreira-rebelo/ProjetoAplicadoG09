namespace ISI.Application.UseCases.Reservation.CreateReservation;

public class CreateReservationOutput
{
    public string ReservationCode { get; set; }
    public string ReservationPassword { get; set; }

    public CreateReservationOutput(
        string reservationCode,
        string reservationPassword
    )
    {
        ReservationCode = reservationCode;
        ReservationPassword = reservationPassword;
    }

    public static CreateReservationOutput FromDomain(
        Domain.Entity.Reservation reservation
    ) => new(
        reservation.Id,
        reservation.ReservationPassword
    );
}