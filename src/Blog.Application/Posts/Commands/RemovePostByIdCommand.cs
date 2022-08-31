using Blog.Application.Models;
using MediatR;

namespace Blog.Application.Posts.Commands
{
    public class RemovePostByIdCommand : IRequest<OperationResult<bool>>
    {
        public Guid PostId { get; set; }
    }
}
