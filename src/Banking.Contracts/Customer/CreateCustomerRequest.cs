namespace Banking.Contracts.Customer
{
    public record CreateCustomerRequest(
        string FirstName,
        string LastName,
        string Email,
        string SSN);
}