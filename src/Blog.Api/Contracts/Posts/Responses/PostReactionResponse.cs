using Blog.Domain.Aggregates.PostAggregate;

namespace Blog.Api.Contracts.Posts.Responses
{
    public class PostReactionResponse
    {
        public Guid PostReactionId { get; set; }
        public Guid PostId { get; set; }
        public Guid UserProfileId { get; set; }
        public ReactionType ReactionType { get; set; }
        public string UserFullName { get; set; }
    }
}
