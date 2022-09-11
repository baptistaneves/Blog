using Blog.Application.Models;
using Blog.Domain.Aggregates.PostAggregate;
using MediatR;

namespace Blog.Application.Posts.Commands
{
    public class AddCommentAnswerCommand : IRequest<OperationResult<CommentAnswer>>
    {
        public Guid PostId { get; set; }
        public Guid CommentId { get; set; }
        public Guid UserProfileId { get; set; }
        public string Text { get; set; }
    }
}
