using Blog.Application.Models;
using Blog.Domain.Aggregates.UserProfileAggregate;
using MediatR;

namespace Blog.Application.UserProfiles.Queries
{
    public class GetAllAdminUserProfilesQuery : IRequest<OperationResult<IEnumerable<UserProfile>>>
    {
        public Guid CurrentUserProfileId { get; set; }
    }
}
