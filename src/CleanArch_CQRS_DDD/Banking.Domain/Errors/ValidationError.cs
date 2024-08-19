using Banking.SharedKernel.Error;

namespace Banking.Domain.Errors
{
    public static class ValidationError
    {
        public static Error ParameterError(string parameterName) => new(ErrorCode.BadRequest, [$"{parameterName} is required"]);
        public static Error ParameterPatternError(string parameterName) => new(ErrorCode.BadRequest, [$"Value does not match the {parameterName} pattern."]);
    }
}