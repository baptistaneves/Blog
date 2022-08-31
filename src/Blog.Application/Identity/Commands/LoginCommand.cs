using Blog.Application.Identity.Dtos;
using Blog.Application.Models;
using MediatR;

namespace Blog.Application.Identity.Commands
{
    public class LoginCommand : IRequest<OperationResult<IdentityUserProfileDto>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
