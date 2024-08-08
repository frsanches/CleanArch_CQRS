namespace Banking.Application.Utils
{
    public interface ICommandHandler<TCommand, TResult>
        where TCommand : ICommand
    {
        TResult Execute(TCommand command);
    }
}