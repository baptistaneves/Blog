using Blog.Application.Models;
using MediatR;

namespace Blog.Application.Identity.Commands
{
    public class RemoveUserAccountCommand : IRequest<OperationResult<bool>>
    {
        public Guid IdentityUserId { get; set; }
    }
}
