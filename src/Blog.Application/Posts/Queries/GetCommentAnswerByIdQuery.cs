using Blog.Application.Models;
using Blog.Domain.Aggregates.PostAggregate;
using MediatR;

namespace Blog.Application.Posts.Queries
{
    public class GetCommentAnswerByIdQuery : IRequest<OperationResult<CommentAnswer>>
    {
        public Guid PostId { get; set; }
        public Guid CommentId { get; set; }
        public Guid CommentAnswerId { get; set; }
    }
}
