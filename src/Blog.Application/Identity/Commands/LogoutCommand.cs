using Blog.Application.Models;
using MediatR;

namespace Blog.Application.Identity.Commands
{
    public class LogoutCommand : IRequest<OperationResult<bool>> { }
}
