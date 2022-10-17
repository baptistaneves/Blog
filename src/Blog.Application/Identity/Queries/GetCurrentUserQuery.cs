using Blog.Application.Identity.Dtos;
using Blog.Application.Models;
using MediatR;

namespace Blog.Application.Identity.Queries
{
    public class GetCurrentUserQuery : IRequest<OperationResult<IdentityUserProfileDto>>
    {
        public Guid IdentityId { get; set; }
    }
}
