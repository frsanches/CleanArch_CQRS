namespace Banking.SharedKernel.Error
{
    public record Error(ErrorCode errorCode, string[] messages);
}