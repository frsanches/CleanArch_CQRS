using Banking.Domain.Errors;
using Banking.SharedKernel.Error;
using Banking.SharedKernel.Response;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace Banking.Domain.Entities.Customers
{
    public record struct SSN
    {
        const string ssnPattern = @"^\d{3}-\d{2}-\d{4}$";
        public string Value { get; private set; }

        [JsonConstructor]
        private SSN(string value)
        {
            Value = value;
        }

        public static Result<SSN, Error> Create(string ssn)
        {
            if (!Regex.IsMatch(ssn, ssnPattern))
                return ValidationError.ParameterPatternError(nameof(ssn)); ;
            
            return new SSN(ssn);
        }

        public static SSN FromDB(string ssn)
        {
            return new SSN(ssn);
        }
    }
}