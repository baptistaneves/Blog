using Blog.Application.Models;
using Blog.Domain.Aggregates.PostAggregate;
using MediatR;

namespace Blog.Application.Posts.Commands
{
    public class AddPostCommentCommand : IRequest<OperationResult<PostComment>>
    {
        public string Text { get; set; }
        public Guid PostId { get; set; }
        public Guid UserProfileId { get; set; }
    }
}
