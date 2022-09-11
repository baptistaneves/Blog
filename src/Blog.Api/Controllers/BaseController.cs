using Blog.Api.Contracts.Common;
using Blog.Application.Enums;
using Blog.Application.Models;

namespace Blog.Api.Controllers
{

    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        private readonly ErrorResponse _errorResponse;
        public BaseController()
        {
            _errorResponse = new ErrorResponse();
        }

        protected ActionResult HandleErrorResponse(List<Error> errors)
        {
            _errorResponse.TimeStamp = DateTime.Now;

            if(errors.Any(e => e.Code == ErrorCode.NotFound))
            {
                var error = errors.FirstOrDefault(e=> e.Code == ErrorCode.NotFound);

                _errorResponse.StatusCode = 404;
                _errorResponse.StatusPhrase = "Not Found";
                _errorResponse.Errors.Add(error.Message);

                return NotFound(_errorResponse);
            }

            _errorResponse.StatusCode = 400;
            _errorResponse.StatusPhrase = "Bad Request";
            errors.ForEach(e=> _errorResponse.Errors.Add(e.Message));

            return BadRequest(_errorResponse);
        }

        protected ActionResult HandleErrorResponse()
        {
            _errorResponse.TimeStamp = DateTime.Now;
            _errorResponse.StatusCode = 400;
            _errorResponse.StatusPhrase = "Bad Request";

            return BadRequest(_errorResponse);
        }

        protected void AddError(string errorMessage)
        {
            _errorResponse.Errors.Add(errorMessage);
        }
    }
}
