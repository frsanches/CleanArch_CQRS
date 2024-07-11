namespace Core.Domain.Entities.Customers
{
    public class Custormer : IEntity
    {
        public Guid Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public SSN SSN { get; private set; }

        private Custormer(string firstName, string lastName, string email, SSN ssn)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            SSN = ssn;
        }

        public static Custormer Create(string firstName, string lastName, string email, string ssn)
        {
            if (string.IsNullOrWhiteSpace(firstName)) throw new ArgumentNullException(nameof(firstName));
            if (string.IsNullOrWhiteSpace(lastName)) throw new ArgumentNullException(nameof(lastName));
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentNullException(nameof(email));
            if (string.IsNullOrWhiteSpace(ssn)) throw new ArgumentNullException(nameof(ssn));

            return new Custormer(firstName, lastName, email, new SSN(ssn));
        }
    }
}