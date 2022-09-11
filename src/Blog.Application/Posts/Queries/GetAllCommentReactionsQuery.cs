using Blog.Application.Models;
using Blog.Domain.Aggregates.PostAggregate;
using MediatR;

namespace Blog.Application.Posts.Queries
{
    public class GetAllCommentReactionsQuery : IRequest<OperationResult<IEnumerable<CommentReaction>>>
    {
        public Guid PostId { get; set; }
        public Guid PostCommentId { get; set; }
    }
}
