using Blog.Api.Contracts.Common;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Blog.Api.Filters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if(!context.ModelState.IsValid)
            {
                var errorResponse = new ErrorResponse();

                errorResponse.StatusCode = 400;
                errorResponse.StatusPhrase = "Bad Request";
                errorResponse.TimeStamp = DateTime.Now;

                var errors = context.ModelState.Values.SelectMany(v => v.Errors);

                foreach (var error in errors)
                {
                    errorResponse.Errors.Add(error.ErrorMessage);
                }
                context.Result = new BadRequestObjectResult(errorResponse);
            }
        }
    }
}
