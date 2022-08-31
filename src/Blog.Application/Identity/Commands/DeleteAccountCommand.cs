using Blog.Application.Models;
using MediatR;

namespace Blog.Application.Identity.Commands
{
    public class DeleteAccountCommand : IRequest<OperationResult<bool>>
    {
        public Guid identityUserId { get; set; }
        public Guid RequestorId { get; set; }
    }
}
