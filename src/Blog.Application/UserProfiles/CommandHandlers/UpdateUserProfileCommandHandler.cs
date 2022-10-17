using Blog.Application.Enums;
using Blog.Application.Identity;
using Blog.Application.Models;
using Blog.Application.UserProfiles.Commands;
using Blog.Dal.Context;
using Blog.Domain.Aggregates.UserProfileAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blog.Application.UserProfiles.CommandHandlers
{
    public class UpdateUserProfileCommandHandler : IRequestHandler<UpdateUserProfileCommand, OperationResult<bool>>
    {
        private readonly DataContext _context;
        private readonly OperationResult<bool> _result;
        public UpdateUserProfileCommandHandler(DataContext context)
        {
            _context = context;
            _result = new OperationResult<bool>();
        }

        public async Task<OperationResult<bool>> Handle(UpdateUserProfileCommand request, 
            CancellationToken cancellationToken)
        {
            try
            {
                var identityUser = await _context.Users
                      .FirstOrDefaultAsync(i => i.Id == request.IdentityId, cancellationToken);

                if (identityUser is null)
                {
                    _result.AddError(ErrorCode.NotFound, IdentityErrorMessages.IdentityUserNotFound);
                    return _result;
                }

                var userProfile = await _context.UserProfiles.AsNoTracking()
                      .FirstOrDefaultAsync(up => up.IdentityId == request.IdentityId, cancellationToken);

                if (userProfile is null)
                {
                    _result.AddError(ErrorCode.NotFound, 
                        string.Format(UserProfileErrorMessages.UserProfileNotFound, request.IdentityId));                    return _result;
                }

                identityUser.Email = request.EmailAddress;

                var basicInfo = BasicInfo.CreateBasicInfo(request.FirstName, request.LastName, 
                    request.EmailAddress, request.Phone);

                userProfile.UpdateBasicInfo(basicInfo);

                _context.Users.Update(identityUser);
                _context.UserProfiles.Update(userProfile);
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
