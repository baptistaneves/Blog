using Blog.Application.Models;
using Blog.Application.Posts.Dtos;
using MediatR;

namespace Blog.Application.Posts.Queries
{
    public class GetAllPostsQuery : IRequest<OperationResult<IEnumerable<PostDto>>> 
    { 
    }
}
