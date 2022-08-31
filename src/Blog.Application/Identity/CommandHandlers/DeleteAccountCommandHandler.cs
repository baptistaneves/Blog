using Blog.Application.Enums;
using Blog.Application.Identity.Commands;
using Blog.Application.Models;
using Blog.Dal.Context;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Blog.Application.Identity.CommandHandlers
{
    public class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountCommand, OperationResult<bool>>
    {
        private readonly DataContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly OperationResult<bool> _result;

        public DeleteAccountCommandHandler(DataContext context, 
                                           UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            _result = new OperationResult<bool>();
        }

        public async Task<OperationResult<bool>> Handle(DeleteAccountCommand request, 
            CancellationToken cancellationToken)
        {
            try
            {
                var identityUser = await _context.Users
                      .FirstOrDefaultAsync(i=> i.Id == request.identityUserId.ToString(), cancellationToken);

                if(identityUser is null)
                {
                    _result.AddError(ErrorCode.NotFound, IdentityErrorMessages.IdentityUserNotFound);
                    return _result;
                }

                var userProfile = await _context.UserProfiles
                    .FirstOrDefaultAsync(up => up.IdentityId == identityUser.Id, cancellationToken);

                if(userProfile is null)
                {
                    _result.AddError(ErrorCode.NotFound, IdentityErrorMessages.UserProfileNotFound);
                    return _result;
                }

                if(identityUser.Id != request.RequestorId.ToString())
                {
                    _result.AddError(ErrorCode.UnathorizedAccountRemoval, IdentityErrorMessages.UnathorizedAccountRemoval);
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
