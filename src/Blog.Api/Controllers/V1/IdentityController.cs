using Blog.Api.Contracts.Identity.Request;
using Blog.Api.Contracts.Identity.Response;
using Blog.Application.Identity.Commands;
using Blog.Application.Identity.Queries;

namespace Blog.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route(ApiRoutes.BaseRoute)]
    public class IdentityController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public IdentityController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet, Route(ApiRoutes.Identity.GetCurrentUser)]
        public async Task<IActionResult> GetCurrentUser(CancellationToken token)
        {
            var query = new GetCurrentUserQuery { IdentityId = HttpContext.GetIdentityIdClaimValue() };

            var result = await _mediator.Send(query, token);
            if(result.IsError) return HandleErrorResponse(result.Errors);

            var mapped = _mapper.Map<IdentityUserProfileResponse>(result.Payload);

            return Ok(mapped);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet, Route(ApiRoutes.Identity.GetAllRoles)]
        public async Task<IActionResult> GetAllRoles(CancellationToken token)
        {
            var query = new GetAllRolesQuery();

            var result = await _mediator.Send(query, token);
            if (result.IsError) return HandleErrorResponse(result.Errors);

            return Ok(result.Payload);
        }

        [HttpPost, Route(ApiRoutes.Identity.Register)]
        [ValidateModel]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequest registration, 
            CancellationToken token)
        {
            var command = _mapper.Map<RegisterIdentityCommand>(registration);

            var result = await _mediator.Send(command, token);
            if (result.IsError) return HandleErrorResponse(result.Errors);

            var mapped = _mapper.Map<IdentityUserProfileResponse>(result.Payload);

            return Ok(mapped);
        }

        [HttpPost, Route(ApiRoutes.Identity.CreateUser)]
        [ValidateModel]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest registration, 
            CancellationToken token)
        {
            var command = _mapper.Map<RegisterIdentityCommand>(registration);

            var result = await _mediator.Send(command, token);
            if (result.IsError) return HandleErrorResponse(result.Errors);

            return Ok(_mapper.Map<IdentityUserProfileResponse>(result.Payload));
        }

        [HttpPost, Route(ApiRoutes.Identity.Login)]
        [ValidateModel]
        public async Task<IActionResult> Login([FromBody] Login login, CancellationToken token)
        {
            var command = new LoginCommand { UserName = login.UserName, Password = login.Password};

            var result = await _mediator.Send(command, token);
            if (result.IsError) return HandleErrorResponse(result.Errors);

            return Ok(_mapper.Map<IdentityUserProfileResponse>(result.Payload));
        }

        [HttpGet, Route(ApiRoutes.Identity.Logout)]
        public async Task<IActionResult> Logout(CancellationToken token)
        {
            var command = new LogoutCommand();

            var result = await _mediator.Send(command, token);
            if (result.IsError) return HandleErrorResponse(result.Errors);

            return NoContent();
        }

        [HttpDelete, Route(ApiRoutes.Identity.DeleteAccount)]
        [Authorize(Roles = "Admin,User")]
        [ValidateGuid("identityUserId")]
        public async Task<IActionResult> DeleteAccount(string identityUserId, CancellationToken token)
        {
            var identityIdGuid = Guid.Parse(identityUserId);
            var command = new DeleteAccountCommand
            {
                identityUserId = identityIdGuid,
                RequestorId = HttpContext.GetIdentityIdClaimValue()
            };

            var result = await _mediator.Send(command, token);
            if (result.IsError) return HandleErrorResponse(result.Errors);

            return NoContent();
        }

        [HttpDelete, Route(ApiRoutes.Identity.RemoveUser)]
        [Authorize(Roles = "Admin")]
        [ValidateGuid("identityUserId")]
        public async Task<IActionResult> RemoveUserAccount(string identityUserId, CancellationToken token)
        {
            var identityIdGuid = Guid.Parse(identityUserId);
            var command = new RemoveUserAccountCommand { IdentityUserId = identityIdGuid };

            var result = await _mediator.Send(command, token);
            if (result.IsError) return HandleErrorResponse(result.Errors);

            return NoContent();
        }
    }
}
