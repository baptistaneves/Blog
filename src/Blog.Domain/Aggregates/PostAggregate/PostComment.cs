using Blog.Domain.Aggregates.UserProfileAggregate;
using Blog.Domain.Exceptions.Posts;
using Blog.Domain.Validatores.Posts;

namespace Blog.Domain.Aggregates.PostAggregate
{
    public class PostComment
    {
        private readonly List<CommentAnswer> _answers = new List<CommentAnswer>();
        private readonly List<CommentReaction> _reactions = new List<CommentReaction>();

        private PostComment() { }

        public Guid PostCommentId { get; private set; }
        public Guid UserProfileId { get; private set; }
        public Guid PostId { get; private set; }
        public string Text { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime LastModified { get; private set; }

        //EF - Relation
        public UserProfile UserProfile { get; private set; }
        public Post Post { get; private set; }
        public IEnumerable<CommentAnswer> Answers { get { return _answers; } }
        public IEnumerable<CommentReaction> Reactions { get { return _reactions; } }

        /// <summary>
        /// Create post comment
        /// </summary>
        /// <param name="postId">Post ID</param>
        /// <param name="userProfileId">User profile ID</param>
        /// <param name="text">Text comment</param>
        /// <returns see cref="PostComment"></returns>
        /// <exception cref="PostCommentNotValidException"></exception>
        public static PostComment CreatePostComment(Guid postId, Guid userProfileId, string text)
        {
            var validator = new PostCommentValidator();

            var objToValidate = new PostComment
            {
                PostId = postId,
                UserProfileId = userProfileId,
                Text = text,
                CreatedAt = DateTime.UtcNow,
                LastModified = DateTime.UtcNow
            };

            var validationResult = validator.Validate(objToValidate);
            if (validationResult.IsValid) return objToValidate;

            var exception = new PostCommentNotValidException("Erro ao criar comentário de notícia");

            validationResult.Errors.ForEach(error => exception.ValidationErrors.Add(error.ErrorMessage));

            throw exception;
        }

        /// <summary>
        /// Update post comment
        /// </summary>
        /// <param name="postId">Post ID</param>
        /// <param name="userProfileId">User profile ID</param>
        /// <param name="text">Text comment</param>
        /// <returns see cref="PostComment"></returns>
        /// <exception cref="PostCommentNotValidException"></exception>
        public void UpdatePostComment(string updatedComment)
        {
            if(!string.IsNullOrWhiteSpace(updatedComment))
            {
                var exception = new PostCommentNotValidException("Erro ao actualizar comentário de notícia");

                exception.ValidationErrors.Add("O texto digitado é nulo ou possui espaços vázios");

                throw exception;
            }

            Text = updatedComment;
            LastModified = DateTime.UtcNow;
        }

        public void AddCommetAnswer(CommentAnswer response)
        {
            _answers.Add(response);
        }

        public void RemoveCommentAnswer(CommentAnswer toRemove)
        {
            _answers.Remove(toRemove);
        }

        public void UpdateCommentAnswer(Guid commentResponseId, string updatedText)
        {
            var commentResponse = _answers.FirstOrDefault(r => r.CommentAnswerId == commentResponseId);
            if(commentResponse != null)
                commentResponse.UpdateCommentAnswerText(updatedText);
        }

        public void AddReactionComment(CommentReaction reaction)
        {
            _reactions.Add(reaction);
        }

        public void RemoveCommentReaction(CommentReaction toRemove)
        {
            _reactions.Remove(toRemove);
        }
    }
}
