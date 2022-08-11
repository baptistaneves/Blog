using Blog.Domain.Aggregates.UserProfileAggregate;

namespace Blog.Domain.Aggregates.PostAggregate
{
    public class PostReaction
    {
        private PostReaction() {  }

        public Guid PostReactionId { get; private set; }
        public Guid PostId { get; private set; }
        public Guid UserProfileId { get; private set; }
        public ReactionType ReactionType { get; private set; }

        //EF - Relation
        public UserProfile UserProfile { get; private set; }
        public Post Post { get; private set; }

        /// <summary>
        /// Create post reaction
        /// </summary>
        /// <param name="postId">Post ID</param>
        /// <param name="userProfileId">User profile ID</param>
        /// <param name="reactionType">Reaction Type</param>
        /// <returns see cref="PostReaction"></returns>
        public static PostReaction CreatePostReaction(Guid postId, Guid userProfileId, 
            ReactionType reactionType)
        {
            return new PostReaction
            {
                PostId = postId,
                UserProfileId = userProfileId,
                ReactionType = reactionType
            };
        }
    }

}
