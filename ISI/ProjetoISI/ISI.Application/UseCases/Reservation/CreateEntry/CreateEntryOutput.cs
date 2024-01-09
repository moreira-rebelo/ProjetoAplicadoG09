namespace ISI.Application.UseCases.Reservation.CreateEntry;

public class CreateEntryOutput
{
    public DateTime AccessTime { get; }

    public CreateEntryOutput(DateTime accessTime)
    {
        AccessTime = accessTime;
    }
}