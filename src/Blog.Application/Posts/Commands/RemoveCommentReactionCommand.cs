using Blog.Application.Models;
using MediatR;

namespace Blog.Application.Posts.Commands
{
    public class RemoveCommentReactionCommand : IRequest<OperationResult<bool>>
    {
        public Guid PostId { get; set; }
        public Guid PostCommentId { get; set; }
        public Guid CommentReactionId { get; set; }
    }
}
