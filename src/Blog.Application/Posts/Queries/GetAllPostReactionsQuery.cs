using Blog.Application.Models;
using Blog.Domain.Aggregates.PostAggregate;
using MediatR;

namespace Blog.Application.Posts.Queries
{
    public class GetAllPostReactionsQuery : IRequest<OperationResult<IEnumerable<PostReaction>>>
    {
        public Guid PostId { get; set; }
    }
}
