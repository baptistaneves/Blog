using Blog.Application.Models;
using MediatR;

namespace Blog.Application.Posts.Commands
{
    public class UpdateCommentAnswerCommand : IRequest<OperationResult<bool>>
    {
        public Guid PostId { get; set; }
        public Guid CommentId { get; set; }
        public Guid CommentAnswerId { get; set; }
        public string UpdatedText { get; set; }
    }
}
