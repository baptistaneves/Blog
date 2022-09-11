using Blog.Application.Models;
using Blog.Domain.Aggregates.PostAggregate;
using MediatR;

namespace Blog.Application.Posts.Queries
{
    public class GetPostCommentByIdQuery : IRequest<OperationResult<PostComment>>
    {
        public Guid PostCommentId { get; set; }
        public Guid PostId { get; set; }
    }
}
