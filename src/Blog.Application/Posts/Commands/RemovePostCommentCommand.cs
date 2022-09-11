using Blog.Application.Models;
using MediatR;

namespace Blog.Application.Posts.Commands
{
    public class RemovePostCommentCommand : IRequest<OperationResult<bool>>
    {
        public Guid PostId { get; set; }
        public Guid PostCommentId { get; set; }
    }
}
