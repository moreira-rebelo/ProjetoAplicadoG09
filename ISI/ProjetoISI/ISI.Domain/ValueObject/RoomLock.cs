using ISI.Domain.Validation;

namespace ISI.Domain.ValueObject;

public class RoomLock
{
    public string AccessCode { get; }
    
    public RoomLock(string accessCode)
    {
        AccessCode = accessCode;
        Validate();
    }
    
    public bool CompareAccessCode(string accessCode)
    {
        return AccessCode == accessCode;
    }
    
    private void Validate()
    {
        DomainValidation.NotNullOrEmpty(AccessCode, nameof(AccessCode));
        DomainValidation.MinLength(AccessCode, 6, nameof(AccessCode));
        DomainValidation.MaxLength(AccessCode, 6, nameof(AccessCode));

    }
}