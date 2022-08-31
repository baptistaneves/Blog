using AutoMapper;
using Blog.Application.Enums;
using Blog.Application.Identity.Commands;
using Blog.Application.Identity.Dtos;
using Blog.Application.Models;
using Blog.Application.Services;
using Blog.Dal.Context;
using Blog.Domain.Aggregates.UserProfileAggregate;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Blog.Application.Identity.CommandHandlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, OperationResult<IdentityUserProfileDto>>
    {
        private readonly DataContext _context;
        private readonly SignInManager<IdentityUser> _siginManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly OperationResult<IdentityUserProfileDto> _result;
        private readonly IdentityService _identityService;
        private readonly IMapper _mapper;

        public LoginCommandHandler(DataContext context,
                                   SignInManager<IdentityUser> siginManager,
                                   UserManager<IdentityUser> userManager, 
                                   IMapper mapper,
                                   IdentityService identityService)
        {
            _context = context;
            _result = new OperationResult<IdentityUserProfileDto>();
            _siginManager = siginManager;
            _userManager = userManager;
            _mapper = mapper;
            _identityService = identityService;
        }

        public async Task<OperationResult<IdentityUserProfileDto>> Handle(LoginCommand request, 
            CancellationToken cancellationToken)
        {
            try
            {
                var identityUser = await _userManager.FindByEmailAsync(request.UserName);
                if (identityUser is null)
                {
                    _result.AddError(ErrorCode.IncorrectUserName, IdentityErrorMessages.IncorrectUserName);
                    return _result;
                }

                var siginResult = await _siginManager.CheckPasswordSignInAsync(identityUser, request.Password, true);
                
                if(siginResult.IsLockedOut)
                {
                    _result.AddError(ErrorCode.LockoutOnFailure, IdentityErrorMessages.LockoutOnFailure);
                    return _result;
                }
                
                if(!siginResult.Succeeded)
                {
                    _result.AddError(ErrorCode.IncorrectPassword, IdentityErrorMessages.IncorrectPassword);
                    return _result;
                }

                var userProfile = await _context.UserProfiles.Include(up=> up.BasicInfo)
                    .FirstOrDefaultAsync(up => up.IdentityId == identityUser.Id, cancellationToken);

                _result.Payload = _mapper.Map<IdentityUserProfileDto>(userProfile);
                _result.Payload.Token = await GetJwtString(identityUser, userProfile);
                _result.Payload.UserName = identityUser.UserName;
            }
            catch (Exception ex)
            {
                _result.AddUnknownError($"{ ex.Message }");
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
