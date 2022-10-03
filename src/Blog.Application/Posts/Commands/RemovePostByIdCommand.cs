using Blog.Application.Models;
using Blog.Domain.Aggregates.PostAggregate;
using MediatR;

namespace Blog.Application.Posts.Commands
{
    public class RemovePostByIdCommand : IRequest<OperationResult<Post>>
    {
        public Guid PostId { get; set; }
    }
}
