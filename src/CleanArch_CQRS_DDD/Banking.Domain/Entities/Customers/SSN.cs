using System.Text.RegularExpressions;

namespace Banking.Domain.Entities.Customers
{
    public record SSN
    {
        readonly string ssnPattern = @"^\d{3}-\d{2}-\d{4}$";
        public string Value { get; private set; }

        public SSN(string ssn)
        {
            if (!Regex.IsMatch(ssn, ssnPattern))
                throw new ArgumentException("Value does not match the ssn pattern.");

            Value = ssn;
        }
    }
}