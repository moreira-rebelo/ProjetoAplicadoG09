namespace ISI.infrastructure.Models;

public class ControllerModel
{
    public long Id { get; set; }
    public string LockCode { get; set; }
    
    
    public ControllerModel(
        long id,
        string lockCode
    )
    {
        Id = id;
        LockCode = lockCode;
    }
    
}