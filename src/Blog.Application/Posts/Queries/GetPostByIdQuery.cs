using Blog.Application.Models;
using Blog.Application.Posts.Dtos;
using MediatR;

namespace Blog.Application.Posts.Queries
{
    public class GetPostByIdQuery : IRequest<OperationResult<PostDto>>
    {
        public Guid PostId { get; set; }
    }
}
