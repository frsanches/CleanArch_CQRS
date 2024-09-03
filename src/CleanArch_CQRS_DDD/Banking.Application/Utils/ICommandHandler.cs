using Banking.SharedKernel.Error;
using Banking.SharedKernel.Response;

namespace Banking.Application.Utils
{
    public interface ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        Task<Result<Value,Error>> HandleAsync(TCommand command);
    }
}