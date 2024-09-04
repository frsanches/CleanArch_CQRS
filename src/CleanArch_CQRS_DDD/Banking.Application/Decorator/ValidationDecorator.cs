using Banking.Application.Utils;
using Banking.SharedKernel.Error;
using Banking.SharedKernel.Response;
using FluentValidation;

namespace Banking.Application.Decorator
{
    public sealed class ValidationDecorator<TCommand> : ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        private readonly ICommandHandler<TCommand> _handler;
        private readonly IValidator<TCommand> _validator;

        public ValidationDecorator(
            ICommandHandler<TCommand> handler,
            IValidator<TCommand> validator)
        {
            _handler = handler;
            _validator = validator;
        }
        public async Task<Result<Value, Error>> HandleAsync(TCommand command)
        {
            var validationResult = await _validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(p => p.ErrorMessage).ToArray();
                var error = new Error(ErrorCode.BadRequest, errorMessages);

                return error;
            }

            return await _handler.HandleAsync(command);
        }
    }
}