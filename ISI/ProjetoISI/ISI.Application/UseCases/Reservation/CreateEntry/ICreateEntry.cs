using MediatR;

namespace ISI.Application.UseCases.Reservation.CreateEntry;

public interface ICreateEntry: IRequestHandler<CreateEntryInput, CreateEntryOutput>
{
    
}