using Microsoft.AspNetCore.Mvc;

namespace Banking.Api.ApiError
{
    internal class ApiErrorDetails : ProblemDetails
    {
        public string[] Errors { get; private set; }
        public ApiErrorDetails(int statusCode, string title, string[] errors)
        {
            Title = title;
            Status = statusCode;
            Errors = errors;
        }
    }
}