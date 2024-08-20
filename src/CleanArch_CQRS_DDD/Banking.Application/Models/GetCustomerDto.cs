namespace Banking.Application.Models
{
    public class GetCustomerDto
    {
        public string Id { get; init; } = string.Empty;
        public string FirstName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
        public string SSN { get; init; } = string.Empty;
    }
}