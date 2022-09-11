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
            CreateMap<CreatePost, CreatePostCommand>();
            CreateMap<UpdatePost, UpdatePostCommand>();
            CreateMap<Post, PostResponse>();

            CreateMap<PostComment, PostCommentResponse>()
                .ForMember(dest => dest.UserFullName, opt => 
                   opt.MapFrom(src => src.UserProfile.BasicInfo.FirstName + " " + src.UserProfile.BasicInfo.FirstName));

            CreateMap<PostReaction, PostReactionResponse>()
                .ForMember(dest => dest.UserFullName, opt => 
                   opt.MapFrom(src => src.UserProfile.BasicInfo.FirstName + " " + src.UserProfile.BasicInfo.FirstName));

            CreateMap<CommentAnswer, CommentAnswerResponse>()
                .ForMember(dest => dest.UserFullName, opt =>
                   opt.MapFrom(src => src.UserProfile.BasicInfo.FirstName + " " + src.UserProfile.BasicInfo.FirstName));

            CreateMap<CommentReaction, CommentReactionResponse>()
               .ForMember(dest => dest.UserFullName, opt =>
                  opt.MapFrom(src => src.UserProfile.BasicInfo.FirstName + " " + src.UserProfile.BasicInfo.FirstName));
        }
    }
}
