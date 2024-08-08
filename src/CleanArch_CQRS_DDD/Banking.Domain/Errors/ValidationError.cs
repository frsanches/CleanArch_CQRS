using Banking.SharedKernel.Error;
using System.Net;

namespace Banking.Domain.Errors
{
    public static class ValidationError
    {
        public static Error ParameterError(string parameterName) => new(HttpStatusCode.BadRequest, $"{parameterName} is required");
    }
}