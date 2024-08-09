using Banking.Domain.Errors;
using Banking.SharedKernel.Error;
using Banking.SharedKernel.Response;
using System.Runtime.Intrinsics.X86;

namespace Banking.Domain.Entities.Customers
{
    public class Customer : IEntity
    {
        public Guid Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public SSN SSN { get; private set; }

        private Customer(string firstName, string lastName, string email, SSN ssn)
        {
            Id = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            SSN = ssn;
        }

        private Customer(Guid customerId, string firstName, string lastName, string email, SSN ssn) 
        {
            Id = customerId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            SSN = ssn;
        }

        public static Result<Customer, Error> Create(string firstName, string lastName, string email, string ssn)
        {
            if (string.IsNullOrWhiteSpace(firstName)) return ValidationError.ParameterError(nameof(firstName));
            if (string.IsNullOrWhiteSpace(lastName)) return ValidationError.ParameterError(nameof(lastName));
            if (string.IsNullOrWhiteSpace(email)) return ValidationError.ParameterError(nameof(email));
            if (string.IsNullOrWhiteSpace(ssn)) return ValidationError.ParameterError(nameof(ssn));
            
            var newSsn = SSN.Create(ssn);

            if (!newSsn.IsSuccess)
                return newSsn.Error!;

            return new Customer(firstName, lastName, email, newSsn.Value);
        }

        public static Customer FromDB(Guid customerId, string firstName, string lastName, string email, string ssn)
        {
            return new Customer(customerId, firstName, lastName, email, SSN.FromDB(ssn));
        }
    }
}