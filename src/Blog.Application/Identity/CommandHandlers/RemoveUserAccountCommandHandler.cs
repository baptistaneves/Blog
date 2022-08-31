using Blog.Application.Enums;
using Blog.Application.Identity.Commands;
using Blog.Application.Models;
using Blog.Dal.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blog.Application.Identity.CommandHandlers
{
    public class RemoveUserAccountCommandHandler : IRequestHandler<RemoveUserAccountCommand, OperationResult<bool>>
    {
        private readonly DataContext _context;

        private readonly OperationResult<bool> _result;

        public RemoveUserAccountCommandHandler(DataContext context)
        {
            _context = context;
            _result = new OperationResult<bool>();
        }

        public async Task<OperationResult<bool>> Handle(RemoveUserAccountCommand request, 
            CancellationToken cancellationToken)
        {
            try
            {
                var identityUser = await _context.Users
                      .FirstOrDefaultAsync(i => i.Id == request.IdentityUserId.ToString(), cancellationToken);

                if (identityUser is null)
                {
                    _result.AddError(ErrorCode.NotFound, IdentityErrorMessages.IdentityUserNotFound);
                    return _result;
                }

                var userProfile = await _context.UserProfiles
                    .FirstOrDefaultAsync(up => up.IdentityId == identityUser.Id, cancellationToken);

                if (userProfile is null)
                {
                    _result.AddError(ErrorCode.NotFound, IdentityErrorMessages.UserProfileNotFound);
                    return _result;
                }

                _context.UserProfiles.Remove(userProfile);
                _context.Users.Remove(identityUser);
                await _context.SaveChangesAsync(cancellationToken);

                _result.Payload = true;
            }
            catch (Exception ex)
            {
                _result.AddUnknownError($"{ex.Message}");
            }

            return _result;
        }
    }
}
