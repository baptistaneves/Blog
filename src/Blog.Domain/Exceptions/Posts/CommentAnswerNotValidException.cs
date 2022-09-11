namespace Blog.Domain.Exceptions.Posts
{
    public class CommentAnswerNotValidException : NotValidException
    {
        public CommentAnswerNotValidException() { }

        public CommentAnswerNotValidException(string message) : base(message) { }

        public CommentAnswerNotValidException(string message, Exception inner) : base(message, inner) { }
        
    }
}
