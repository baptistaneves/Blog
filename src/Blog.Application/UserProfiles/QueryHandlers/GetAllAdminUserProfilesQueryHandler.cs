﻿using Blog.Application.Enums;
using Blog.Application.Identity;
using Blog.Application.Models;
using Blog.Application.UserProfiles.Queries;
using Blog.Dal.Context;
using Blog.Domain.Aggregates.UserProfileAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blog.Application.UserProfiles.QueryHandlers
{
    public class GetAllAdminUserProfilesQueryHandler : IRequestHandler<GetAllAdminUserProfilesQuery, 
        OperationResult<IEnumerable<UserProfile>>>
    {
        private readonly DataContext _context;
        private readonly OperationResult<IEnumerable<UserProfile>> _result;
        public GetAllAdminUserProfilesQueryHandler(DataContext context)
        {
            _context = context;
            _result = new OperationResult<IEnumerable<UserProfile>>();
        }
        public async Task<OperationResult<IEnumerable<UserProfile>>> Handle(GetAllAdminUserProfilesQuery request,
            CancellationToken cancellationToken)
        {
            try
            {
                _result.Payload = await _context.UserProfiles.AsNoTracking()
                        .Where(up=> up.UserProfileId != request.CurrentUserProfileId 
                               && up.Role == IdentityUserRoles.ADMIN || up.Role == IdentityUserRoles.EDITOR)
                        .ToListAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _result.AddUnknownError($"{ex.Message}");
            }

            return _result;
        }
    }
}
