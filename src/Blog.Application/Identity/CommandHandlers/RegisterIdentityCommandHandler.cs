using AutoMapper;
using Blog.Application.Enums;
using Blog.Application.Identity.Commands;
using Blog.Application.Identity.Dtos;
using Blog.Application.Models;
using Blog.Application.Services;
using Blog.Dal.Context;
using Blog.Domain.Aggregates.UserProfileAggregate;
using Blog.Domain.Exceptions.UsersProfiles;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Blog.Application.Identity.CommandHandlers
{
    public class RegisterIdentityCommandHandler : IRequestHandler<RegisterIdentityCommand, 
        OperationResult<IdentityUserProfileDto>>
    {
        private readonly DataContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly OperationResult<IdentityUserProfileDto> _result;
        private readonly IMapper _mapper;
        private readonly IdentityService _identityService;

        public RegisterIdentityCommandHandler(UserManager<IdentityUser> userManager,
                                              DataContext context,
                                              IMapper mapper,
                                              RoleManager<IdentityRole> roleManager, 
                                              IdentityService identityService)
        {
            _userManager = userManager;
            _result = new OperationResult<IdentityUserProfileDto>();
            _context = context;
            _mapper = mapper;
            _roleManager = roleManager;
            _identityService = identityService;
        }
        public async Task<OperationResult<IdentityUserProfileDto>> Handle(RegisterIdentityCommand request, 
            CancellationToken cancellationToken)
        {
            try
            {
                var identity = await _userManager.FindByEmailAsync(request.EmailAddress);

                if(identity != null)
                {
                    _result.AddError(ErrorCode.IdentityUserAlreadyExists, IdentityErrorMessages.IdentityUserAlreadyExists);
                    return _result;
                }

                await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

                var identityUser = new IdentityUser
                {
                    UserName = request.EmailAddress,
                    Email = request.EmailAddress,
                    PhoneNumber = request.Phone
                };

                var identityCreationResult = await _userManager.CreateAsync(identityUser, request.Password);
                if(!identityCreationResult.Succeeded)
                {
                    await transaction.RollbackAsync(cancellationToken);

                    foreach (var error in identityCreationResult.Errors)
                    {
                        _result.AddError(ErrorCode.ValidationErrors, error.Description);
                    }

                    return _result;
                }

                request.Role = String.IsNullOrWhiteSpace(request.Role) ? IdentityUserRoles.USER : request.Role;

                var addToRoleResult = await _userManager.AddToRoleAsync(identityUser, request.Role);
                if (!addToRoleResult.Succeeded)
                {
                    await transaction.RollbackAsync(cancellationToken);

                    foreach (var error in addToRoleResult.Errors)
                    {
                        _result.AddError(ErrorCode.ValidationErrors, error.Description);
                    }

                    return _result;
                }

                var basicInfo = BasicInfo.CreateBasicInfo(request.FirstName, request.LastName, 
                    request.EmailAddress, request.Phone);

                var userProfile = UserProfile.CreateUserProfile(identityUser.Id, request.Role, basicInfo);

                _context.UserProfiles.Add(userProfile);

                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);

                _result.Payload = _mapper.Map<IdentityUserProfileDto>(userProfile);
                _result.Payload.Token = await GetJwtString(identityUser, userProfile);
                _result.Payload.UserName = identityUser.UserName;
            }
            catch (UserProfileNotValidException ex)
            {
                ex.ValidationErrors.ForEach(error =>
                {
                    _result.AddError(ErrorCode.ValidationErrors, $"{ error }");
                });
            }
            catch (Exception ex)
            {
                _result.AddUnknownError($"{ ex.Message}");
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
