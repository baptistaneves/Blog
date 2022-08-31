using AutoMapper;
using Blog.Application.Identity.Dtos;
using Blog.Domain.Aggregates.UserProfileAggregate;

namespace Blog.Application.MappingProfiles
{
    public class IdentityMapping : Profile
    {
        public IdentityMapping()
        {
            CreateMap<UserProfile, IdentityUserProfileDto>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.BasicInfo.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.BasicInfo.LastName))
                .ForMember(dest => dest.EmailAddress, opt => opt.MapFrom(src => src.BasicInfo.EmailAddress))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.BasicInfo.Phone));
        }
    }
}
