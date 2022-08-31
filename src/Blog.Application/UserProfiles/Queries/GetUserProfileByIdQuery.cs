using Blog.Application.Models;
using Blog.Domain.Aggregates.UserProfileAggregate;
using MediatR;

namespace Blog.Application.UserProfiles.Queries
{
    public class GetUserProfileByIdQuery : IRequest<OperationResult<UserProfile>>
    {
        public Guid UserProfileId { get; set; }
    }
}
