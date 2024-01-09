using ISI.Domain.ValueObject;
using System;
using System.Collections.Generic;
using System.Data;

namespace ISI.Domain.Entity
{
    public class User : SeedWork.AggregateRoot<Guid>
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
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
        
        public void Update(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
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