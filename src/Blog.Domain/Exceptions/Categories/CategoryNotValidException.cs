namespace Blog.Domain.Exceptions.Categories
{
    public class CategoryNotValidException : NotValidException
    {
        public CategoryNotValidException() {}

        public CategoryNotValidException(string message) : base(message) { }

        public CategoryNotValidException(string message, Exception inner) : base(message, inner) {}
    }
}
