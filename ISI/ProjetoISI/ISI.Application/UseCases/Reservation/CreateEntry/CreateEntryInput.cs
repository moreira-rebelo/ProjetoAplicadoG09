using MediatR;

namespace ISI.Application.UseCases.Reservation.CreateEntry;

public class CreateEntryInput: IRequest<CreateEntryOutput>
{
    public string RoomNumber { get; }
    public string ReservationCode { get; }
    public DateTime AccessTime { get; }

    public CreateEntryInput(
        string roomNumber,
        string reservationCode,
        DateTime accessTime
    )
    {
        RoomNumber = roomNumber;
        ReservationCode = reservationCode;
        AccessTime = accessTime;
    }
}