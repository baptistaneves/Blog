namespace Blog.Domain.Exceptions.Posts
{
    public class PostCommentResponseNotValidException : NotValidException
    {
        public PostCommentResponseNotValidException() { }

        public PostCommentResponseNotValidException(string message) : base(message) { }

        public PostCommentResponseNotValidException(string message, Exception inner) : base(message, inner) { }
        
    }
}
