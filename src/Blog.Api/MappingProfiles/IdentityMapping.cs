using Blog.Api.Contracts.Identity.Request;
using Blog.Api.Contracts.Identity.Response;
using Blog.Application.Identity.Commands;
using Blog.Application.Identity.Dtos;

namespace Blog.Api.MappingProfiles
{
    public class IdentityMapping : Profile
    {
        public IdentityMapping()
        {
            CreateMap<IdentityUserProfileDto, IdentityUserProfileResponse>();
            CreateMap<UserRegistrationRequest, RegisterIdentityCommand>();
            CreateMap<CreateUserRequest, RegisterIdentityCommand>();
        }
    }
}
