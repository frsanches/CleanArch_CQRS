using Banking.SharedKernel.Error;

namespace Banking.Api.ApiError
{
    internal static class ErrorExtension
    {
        internal static ApiErrorDetails ToApiError(this Error error)
        {
            return new((int)error.errorCode, error.title, error.messages);
        }
    }
}