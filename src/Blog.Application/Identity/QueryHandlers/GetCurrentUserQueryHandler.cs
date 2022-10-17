using AutoMapper;
using Blog.Application.Enums;
using Blog.Application.Identity.Dtos;
using Blog.Application.Identity.Queries;
using Blog.Application.Models;
using Blog.Application.Services;
using Blog.Application.UserProfiles;
using Blog.Dal.Context;
using Blog.Domain.Aggregates.UserProfileAggregate;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Blog.Application.Identity.QueryHandlers
{
    public class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, OperationResult<IdentityUserProfileDto>>
    {
        private readonly DataContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;
        private readonly OperationResult<IdentityUserProfileDto> _result;
        private readonly IdentityService _identityService;

        public GetCurrentUserQueryHandler(DataContext context,
                                          UserManager<IdentityUser> userManager,
                                          IMapper mapper, 
                                          IdentityService identityService)
        {
            _context = context;
            _result = new OperationResult<IdentityUserProfileDto>();
            _userManager = userManager;
            _mapper = mapper;
            _identityService = identityService;
        }

        public async Task<OperationResult<IdentityUserProfileDto>> Handle(GetCurrentUserQuery request, 
            CancellationToken cancellationToken)
        {
            try
            {
                var identity = await _userManager.FindByIdAsync(request.IdentityId.ToString());

                if (identity == null)
                {
                    _result.AddError(ErrorCode.NotFound, IdentityErrorMessages.IdentityUserNotFound);
                    return _result;
                }

                var userProfile = await _context.UserProfiles
                    .FirstOrDefaultAsync(up => up.IdentityId == request.IdentityId.ToString(), cancellationToken);

                if (userProfile == null)
                {
                    _result.AddError(ErrorCode.NotFound, UserProfileErrorMessages.UserProfileNotFound);
                    return _result;
                }

                _result.Payload = _mapper.Map<IdentityUserProfileDto>(userProfile);
                _result.Payload.Token = await GetJwtString(identity, userProfile);
                _result.Payload.UserName = identity.UserName;
            }
            catch (Exception ex)
            {
                _result.AddUnknownError($"{ex.Message}");
            }

            return _result;
        }

        private async Task<string> GetJwtString(IdentityUser identityUser, UserProfile userProfile)
        {
            var claimsIdentity = new ClaimsIdentity(new Claim[]
             {
                new Claim(JwtRegisteredClaimNames.Sub, identityUser.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, identityUser.Email),
                new Claim("UserName", identityUser.UserName),
                new Claim("IdentityId", identityUser.Id),
                new Claim("UserProfileId", userProfile.UserProfileId.ToString())
             });

            var userRoles = await _userManager.GetRolesAsync(identityUser);
            foreach (var role in userRoles)
            {
                claimsIdentity.AddClaim(new Claim("role", role));
            }

            var token = _identityService.CreateSecurityToken(claimsIdentity);
            return _identityService.WritenToken(token);
        }
    }
}
