using MediatR;

namespace ISI.Application.UseCases.User.GetUser;

public class GetUserInput: IRequest<GetUserOutput>
{
    public string Email { get;  }
    public GetUserInput(string email) => Email = email;
    
}