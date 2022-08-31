using Blog.Api.Contracts.Posts.Requests;
using Blog.Api.Contracts.Posts.Responses;
using Blog.Application.Posts.Commands;
using Blog.Domain.Aggregates.PostAggregate;

namespace Blog.Api.MappingProfiles
{
    public class PostMapping : Profile
    {
        public PostMapping()
        {
            CreateMap<CreatePostRequest, CreatePostCommand>();
            CreateMap<UpdatePostRequest, UpdatePostCommand>();
            CreateMap<Post, PostResponse>();
        }
    }
}
