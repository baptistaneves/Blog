using Blog.Application.Identity.Dtos;
using Blog.Application.Models;
using MediatR;

namespace Blog.Application.Identity.Commands
{
    public class RegisterIdentityCommand : IRequest<OperationResult<IdentityUserProfileDto>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Role { get; set; }
    }
}
