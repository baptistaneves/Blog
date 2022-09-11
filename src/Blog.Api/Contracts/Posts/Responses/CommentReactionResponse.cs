using Blog.Domain.Aggregates.PostAggregate;

namespace Blog.Api.Contracts.Posts.Responses
{
    public class CommentReactionResponse
    {
        public Guid CommentReactionId { get; set; }
        public Guid PostCommentId { get; set; }
        public Guid UserProfileId { get; set; }
        public string UserFullName { get; set; }
        public ReactionType ReactionType { get; set; }
    }
}
