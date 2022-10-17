using Blog.Api.Contracts.UserProfiles.Request;
using Blog.Api.Contracts.UserProfiles.Response;
using Blog.Application.UserProfiles.Commands;
using Blog.Application.UserProfiles.Queries;

namespace Blog.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route(ApiRoutes.BaseRoute)]
    [Authorize]
    public class UserProfileController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public UserProfileController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [Authorize(Roles = "Admin,Editor")]
        [HttpGet, Route(ApiRoutes.UserProfile.GetPublicUserProfiles)]
        public async Task<IActionResult> GetAllRegisteredUserProfiles(CancellationToken token)
        {
            var query = new GetAllResgisteredUserProfilesQuery();
            var result = await _mediator.Send(query, token);
            if (result.IsError) return HandleErrorResponse(result.Errors);

            return Ok(_mapper.Map<IEnumerable<UserProfileResponse>>(result.Payload));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet, Route(ApiRoutes.UserProfile.GetAdminUserProfiles)]
        public async Task<IActionResult> GetAllAdminUserProfiles(CancellationToken token)
        {
            var currentUserProfileId = HttpContext.GetUserProfileIdCliamValue();
            var query = new GetAllAdminUserProfilesQuery { CurrentUserProfileId = currentUserProfileId };

            var result = await _mediator.Send(query, token);
            if (result.IsError) return HandleErrorResponse(result.Errors);

            return Ok(_mapper.Map<IEnumerable<UserProfileResponse>>(result.Payload));
        }
        
        [HttpGet, Route(ApiRoutes.UserProfile.GetUserProfileById)]
        [Authorize(Roles = "Admin,Editor,User")]
        [ValidateGuid("userProfileId")]
        public async Task<IActionResult> GetUserProfileById(string userProfileId, CancellationToken token)
        {
            var userProfileIdGuid = Guid.Parse(userProfileId);
            var query = new GetUserProfileByIdQuery { UserProfileId = userProfileIdGuid };
            var result = await _mediator.Send(query, token);
            if (result.IsError) return HandleErrorResponse(result.Errors);

            return Ok(_mapper.Map<UserProfileResponse>(result.Payload));
        }

        
        [HttpPatch, Route(ApiRoutes.UserProfile.UpdateUserProfile)]
        [Authorize(Roles = "Admin,Editor,User")]
        [ValidateGuid("identityId")]
        [ValidateModel]
        public async Task<IActionResult> UpdateUserProfile(string identityId, [FromBody] UpdateUserProfileRequest updatedUser,
             CancellationToken token)
        {
            var command = _mapper.Map<UpdateUserProfileCommand>(updatedUser);
            command.IdentityId = identityId;

            var result = await _mediator.Send(command, token);
            if (result.IsError) return HandleErrorResponse(result.Errors);

            return NoContent();
        }
    }
}
