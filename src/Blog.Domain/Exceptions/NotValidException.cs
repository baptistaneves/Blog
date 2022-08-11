namespace Blog.Domain.Exceptions
{
    public class NotValidException : Exception
    {
        internal NotValidException() {}

        internal NotValidException(string message) : base(message) { }

        internal NotValidException(string message, Exception inner) : base(message, inner) { }

        public List<string> ValidationErrors { get; }
    }
}
