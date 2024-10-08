namespace Banking.Contracts.Customer
{
    public record CreateCustomerResponse(
        string Id,
        string FirstName,
        string LastName,
        string Email,
        string SSN);
}