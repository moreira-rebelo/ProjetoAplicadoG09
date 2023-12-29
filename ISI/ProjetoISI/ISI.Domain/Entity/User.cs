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
        
        public User(string firstName, string lastName, Email email)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            CreatedAt = DateTime.UtcNow;
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