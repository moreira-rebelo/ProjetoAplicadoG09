using ISI.Domain.SeedWork;
using ISI.Domain.ValueObject;

namespace ISI.Domain.Entity;

public class Room: SeedWork.Entity
{
    public string RoomNumber { get; }
    public RoomLock RoomLock { get; }
    public long ControllerId { get;}
    
    public Room(string roomNumber, RoomLock roomLock , long controllerId)
    {
        RoomNumber = roomNumber;
        RoomLock = roomLock;
        ControllerId = controllerId;
        Validate();
    }
    
    public bool CompareAccessCode(string accessCode)
    {
        return RoomLock.CompareAccessCode(accessCode);
    }

    private void Validate()
    {
        Validation.DomainValidation.NotNullOrEmpty(RoomNumber, nameof(RoomNumber));
    }
    
    
}