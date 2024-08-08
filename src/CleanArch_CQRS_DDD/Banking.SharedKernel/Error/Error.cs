using System.Net;

namespace Banking.SharedKernel.Error
{
    public record Error(HttpStatusCode statuscode, string message);
}