using Blog.Application.Models;
using MediatR;

namespace Blog.Application.UserProfiles.Commands
{
    public class UpdateUserProfileCommand : IRequest<OperationResult<bool>>
    {
        public Guid UserProfileId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Phone { get; set; }
    }
}
