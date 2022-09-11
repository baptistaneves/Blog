using Blog.Domain.Aggregates.UserProfileAggregate;

namespace Blog.Domain.Aggregates.PostAggregate
{
    public class CommentReaction
    {
        private CommentReaction() { }

        public Guid CommentReactionId { get; private set; }
        public Guid PostCommentId { get; private set; }
        public Guid UserProfileId { get; private set; }
        public ReactionType ReactionType { get; private set; }

        //EF - Relation
        public UserProfile UserProfile { get; private set; }
        public PostComment PostComment { get; private set; }

        /// <summary>
        /// Create comment reaction
        /// </summary>
        /// <param name="userProfileId">User profile Id</param>
        /// <param name="commentId">Comment Id</param>
        /// <param name="reactionType">Reaction type</param>
        /// <returns see cref="CommentReaction"></returns>
        public static CommentReaction CreateCommentReaction(Guid userProfileId, Guid commentId, 
            ReactionType reactionType)
        {
            return new CommentReaction
            {
                UserProfileId = userProfileId,
                PostCommentId = commentId,
                ReactionType = reactionType
            };
        }
    }
}
