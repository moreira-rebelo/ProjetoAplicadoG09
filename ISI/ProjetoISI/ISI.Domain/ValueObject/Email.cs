using ISI.Domain.Validation;

namespace ISI.Domain.ValueObject;

public class Email
{
    public string Value { get; private set; }

    public Email(string value)
    {
        Value = value;
        Validate();
    }

    private void Validate()
    {
        DomainValidation.NotNullOrEmpty(Value, nameof(Value));
        DomainValidation.MaxLength(Value, 255, nameof(Value));
        DomainValidation.MatchRegex(Value, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", nameof(Value));
    }
}