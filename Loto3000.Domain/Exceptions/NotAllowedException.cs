namespace Loto3000.Domain.Exceptions
{
    public class NotAllowedException : Exception
    {
        public NotAllowedException() { }
        public NotAllowedException(string? message) : base(message) { }
        public NotAllowedException(string? message, Exception? innerException)
            : base(message, innerException) { }
    }
}