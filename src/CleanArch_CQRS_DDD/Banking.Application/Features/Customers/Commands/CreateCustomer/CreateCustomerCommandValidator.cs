using Banking.Application.Validator;
using FluentValidation;

namespace Banking.Application.Features.Customers.Commands.CreateCustomer
{
    internal class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator()
        {
            RuleFor(p => p.FirstName)
                .NotEmpty()
                .WithMessage($"{nameof(CreateCustomerCommand.FirstName)} is required.");

            RuleFor(p => p.LastName)
                .NotEmpty()
                .WithMessage($"{nameof(CreateCustomerCommand.LastName)} is required.");

            RuleFor(p => p.Email)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .EmailAddress()
                .When(p => !string.IsNullOrEmpty(p.Email), ApplyConditionTo.CurrentValidator)
                .WithMessage("{PropertyName} is not a valid email address");

            RuleFor(p => p.SSN)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .IsValidSSN()
                .When(p => !string.IsNullOrEmpty(p.SSN), ApplyConditionTo.CurrentValidator);
        }
    }
}