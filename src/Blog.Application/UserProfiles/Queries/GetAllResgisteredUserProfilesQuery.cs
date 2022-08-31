using Blog.Application.Models;
using Blog.Domain.Aggregates.UserProfileAggregate;
using MediatR;

namespace Blog.Application.UserProfiles.Queries
{
    public class GetAllResgisteredUserProfilesQuery : IRequest<OperationResult<IEnumerable<UserProfile>>>
    {
    }
}
