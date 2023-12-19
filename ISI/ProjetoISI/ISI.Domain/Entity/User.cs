using ISI.Domain.ValueObject;
using System;
using System.Collections.Generic;

namespace ISI.Domain.Entity
{
    public class User : SeedWork.AggregateRoot
    {
        public string FirstName { get; }
        public string LastName { get; }
        public Email Email { get; }
        public DateTime CreatedAt { get; }
        
        private List<Guid> _reservations;
        public IReadOnlyList<Guid> Reservations => _reservations.AsReadOnly();
        
        public User(string firstName, string lastName, Email email, List<Guid>? reservations = null)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            CreatedAt = DateTime.UtcNow;
            _reservations = reservations ?? new List<Guid>();
            Validate();
        }
        
        public string GetFullName()
        {
            return $"{FirstName} {LastName}";
        }
        
        private void Validate()
        {
            Validation.DomainValidation.NotNullOrEmpty(FirstName, nameof(FirstName));
            Validation.DomainValidation.NotNullOrEmpty(LastName, nameof(LastName));
            Validation.DomainValidation.NotNull(Email, nameof(Email));
        }
        
    }
}