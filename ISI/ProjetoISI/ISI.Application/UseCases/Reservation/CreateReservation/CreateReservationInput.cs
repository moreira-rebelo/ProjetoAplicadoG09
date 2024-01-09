using MediatR;

namespace ISI.Application.UseCases.Reservation.CreateReservation;

public class CreateReservationInput: IRequest<CreateReservationOutput>
{
    public string FirstName { get; set; }
    public string Email { get; set; }
    public string LastName { get; set; }
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }
    
    public CreateReservationInput(
        string firstName,
        string email,
        string lastName,
        DateTime checkIn,
        DateTime checkOut
    )
    {
        FirstName = firstName;
        Email = email;
        LastName = lastName;
        CheckIn = checkIn;
        CheckOut = checkOut;
    }
}