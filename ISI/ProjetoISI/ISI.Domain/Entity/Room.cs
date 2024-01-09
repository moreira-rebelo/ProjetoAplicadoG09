using System.ComponentModel.DataAnnotations.Schema;
using ISI.Domain.SeedWork;
using ISI.Domain.ValueObject;

namespace ISI.Domain.Entity;

public class Room: SeedWork.Entity<string>
{
    [NotMapped]
    public RoomLock RoomLock { get; }
    public long ControllerId { get;}
    
    public Room(RoomLock roomLock , long controllerId)
    {
        RoomLock = roomLock;
        ControllerId = controllerId;
    }
    
    public bool CompareAccessCode(string accessCode)
    {
        return RoomLock.CompareAccessCode(accessCode);
    }
    
}