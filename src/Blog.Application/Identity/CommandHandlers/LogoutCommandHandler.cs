using Blog.Application.Identity.Commands;
using Blog.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Blog.Application.Identity.CommandHandlers
{
    public class LogoutCommandHandler : IRequestHandler<LogoutCommand, OperationResult<bool>>
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly OperationResult<bool> _result;
        public LogoutCommandHandler(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
            _result = new OperationResult<bool>();
        }

        public async Task<OperationResult<bool>> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _signInManager.SignOutAsync();
            }
            catch (Exception ex)
            {
                _result.AddUnknownError($"{ex.Message}");
            }

            return _result;
        }
    }
}
