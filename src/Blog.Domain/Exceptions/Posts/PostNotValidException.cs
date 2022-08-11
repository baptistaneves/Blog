namespace Blog.Domain.Exceptions.Posts
{
    public class PostNotValidException : NotValidException
    {
        public PostNotValidException() {}

        public PostNotValidException(string message) : base(message) { }

        public PostNotValidException(string message, Exception inner) : base(message, inner){}
    }
}
