using Blog.Application.Models;
using Blog.Application.Posts.Dtos;
using MediatR;

namespace Blog.Application.Posts.Queries
{
    public class GetPostsByCategoryQuery : IRequest<OperationResult<PagedList<PostDto>>>
    {
        public Guid CategoryId { get; set; }
        public PaginationParams Params { get; set; }
    }
}
