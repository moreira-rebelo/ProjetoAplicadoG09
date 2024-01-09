using MediatR;

namespace ISI.Application.UseCases.User.GetUser;

public class GetUserInput: IRequest<GetUserOutput>
{
    public string ReservationCode { get;  }
    public GetUserInput(string reservationCode) => ReservationCode = reservationCode;
    
}