using MediatR;

namespace ISI.Application.UseCases.User.GetUser;

public interface IGetUser:  IRequestHandler<GetUserInput, GetUserOutput>
{
    
}