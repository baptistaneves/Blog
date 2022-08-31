using Blog.Application.Enums;
using Blog.Application.Models;
using Blog.Application.UserProfiles.Queries;
using Blog.Dal.Context;
using Blog.Domain.Aggregates.UserProfileAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blog.Application.UserProfiles.QueryHandlers
{
    public class GetUserProfileByIdQueryHandler : IRequestHandler<GetUserProfileByIdQuery, 
        OperationResult<UserProfile>>
    {
        private readonly DataContext _context;
        private readonly OperationResult<UserProfile> _result;
        public GetUserProfileByIdQueryHandler(DataContext context)
        {
            _context = context;
            _result = new OperationResult<UserProfile>();
        }

        public async Task<OperationResult<UserProfile>> Handle(GetUserProfileByIdQuery request, 
            CancellationToken cancellationToken)
        {
            try
            {
                var userProfile = await _context.UserProfiles.AsNoTracking()
                        .FirstOrDefaultAsync(up=>up.UserProfileId == request.UserProfileId, cancellationToken);

                if(userProfile is null)
                {
                    _result.AddError(ErrorCode.NotFound, 
                        string.Format(UserProfileErrorMessages.UserProfileNotFound, request.UserProfileId));
                    return _result;
                }

                _result.Payload = userProfile;
            }
            catch (Exception ex)
            {
                _result.AddUnknownError($"{ex.Message}");
            }

            return _result;
        }
    }
}
