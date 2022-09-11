using Blog.Application.Models;
using Blog.Domain.Aggregates.PostAggregate;
using MediatR;

namespace Blog.Application.Posts.Queries
{
    public class GetAllPostCommentsQuery : IRequest<OperationResult<IEnumerable<PostComment>>>
    {
        public Guid PostId { get; set; }
    }
}
