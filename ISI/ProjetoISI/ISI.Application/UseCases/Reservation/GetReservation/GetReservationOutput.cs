namespace ISI.Application.UseCases.Reservation.GetReservation;

public class GetReservationOutput
{
    public string ReservationCode { get; }
    public string RoomNumber { get; }
    public DateTime CheckIn { get; }
    public DateTime CheckOut { get; }
    public bool isValid { get; }
    
    private GetReservationOutput(string reservationCode, string roomNumber, DateTime checkIn, DateTime checkOut, bool isValid)
    {
        ReservationCode = reservationCode;
        RoomNumber = roomNumber;
        CheckIn = checkIn;
        CheckOut = checkOut;
        this.isValid = isValid;
    }
    
    public static GetReservationOutput FromReservation(Domain.Entity.Reservation reservation)
    {
        return new GetReservationOutput(reservation.ReservationPassword, reservation.RoomNumber, reservation.CheckIn, reservation.CheckOut, reservation.IsValid());
    }
    
}