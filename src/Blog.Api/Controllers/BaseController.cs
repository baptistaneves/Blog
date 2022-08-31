using Blog.Api.Contracts.Common;
using Blog.Application.Enums;
using Blog.Application.Models;

namespace Blog.Api.Controllers
{

    [ApiController]
    public class BaseController : ControllerBase
    {
        protected ActionResult HandleErrorResponse(List<Error> errors)
        {
            var errorResponse =  new ErrorResponse();
            errorResponse.TimeStamp = DateTime.Now;

            if(errors.Any(e => e.Code == ErrorCode.NotFound))
            {
                var error = errors.FirstOrDefault(e=> e.Code == ErrorCode.NotFound);

                errorResponse.StatusCode = 404;
                errorResponse.StatusPhrase = "Not Found";
                errorResponse.Errors.Add(error.Message);

                return NotFound(errorResponse);
            }

            errorResponse.StatusCode = 400;
            errorResponse.StatusPhrase = "Bad Request";
            errors.ForEach(e=> errorResponse.Errors.Add(e.Message));

            return BadRequest(errorResponse);
        }
    }
}
