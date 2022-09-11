using Blog.Application.Models;
using Blog.Domain.Aggregates.PostAggregate;
using MediatR;

namespace Blog.Application.Posts.Commands
{
    public class AddPostReactionCommand : IRequest<OperationResult<PostReaction>>
    {
        public Guid PostId { get; set; }
        public Guid UserProfileId { get; set; }
        public ReactionType ReactionType { get; set; }
    }
}
