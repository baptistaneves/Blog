using Blog.Api.Contracts.UserProfiles.Request;
using Blog.Api.Contracts.UserProfiles.Response;
using Blog.Application.UserProfiles.Commands;
using Blog.Domain.Aggregates.UserProfileAggregate;

namespace Blog.Api.MappingProfiles
{
    public class UserProfileMapping : Profile
    {
        public UserProfileMapping()
        {
            CreateMap<UpdateUserProfileRequest, UpdateUserProfileCommand>();
            CreateMap<UserProfile, UserProfileResponse>();
            CreateMap<BasicInfo, BasinInformation>();
        }
    }
}
