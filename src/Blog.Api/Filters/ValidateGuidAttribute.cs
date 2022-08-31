using Blog.Api.Contracts.Common;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Blog.Api.Filters
{
    public class ValidateGuidAttribute : ActionFilterAttribute
    {
        private readonly List<string> _keys;
        public ValidateGuidAttribute(params string[] keys)
        {
            _keys = keys.ToList();
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            bool hasError = false;
            var errorRespose = new ErrorResponse();
            _keys.ForEach(key =>
            {
                if (!context.ActionArguments.TryGetValue(key, out var value)) return;
                if(!Guid.TryParse(value?.ToString(), out var guid))
                {
                    hasError = true;
                    errorRespose.Errors.Add($"O formato do identificador {key} não é um GUID válido");
                }
            });

            if(hasError)
            {
                errorRespose.StatusCode = 400;
                errorRespose.StatusPhrase = "Bad Request";
                errorRespose.TimeStamp = DateTime.Now;

                context.Result = new BadRequestObjectResult(errorRespose);
            }
        }
    }
}
