namespace Newscatcher.Client.Contracts.Exceptions;

/// <summary>
/// Ошибка исчерпанного лимита запросов
/// </summary>
public class ToManyRequestsException : Exception
{
    public ToManyRequestsException()
    {
    }

    public ToManyRequestsException(string message)
        : base(message)
    {
    }

    public ToManyRequestsException(string message, Exception inner)
        : base(message, inner)
    {
    }
}