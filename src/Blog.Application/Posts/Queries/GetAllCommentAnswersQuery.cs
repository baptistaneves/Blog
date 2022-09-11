using Blog.Application.Models;
using Blog.Domain.Aggregates.PostAggregate;
using MediatR;

namespace Blog.Application.Posts.Queries
{
    public class GetAllCommentAnswersQuery : IRequest<OperationResult<IEnumerable<CommentAnswer>>>
    {
        public Guid PostId { get; set; }
        public Guid CommentId { get; set; }
    }
}
