using FluentValidation;

namespace Banking.Application.Features.Customers.Commands.ChangeCustomerEmail
{
    internal class ChangeCustomerEmailCommandValidator : AbstractValidator<ChangeCustomerEmailCommand>
    {
        public ChangeCustomerEmailCommandValidator()
        {
            RuleFor(p => p.Email)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .EmailAddress()
                .When(p => !string.IsNullOrEmpty(p.Email), ApplyConditionTo.CurrentValidator)
                .WithMessage("{PropertyName} is not a valid email address");
        }
    }
}
