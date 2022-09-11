using Blog.Application.Models;
using MediatR;

namespace Blog.Application.Posts.Commands
{
    public class RemovePostReactionCommand : IRequest<OperationResult<bool>>
    {
        public Guid PostId { get; set; }
        public Guid PostReactionId { get; set; }
    }
}
