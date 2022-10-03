namespace Blog.Api.Contracts.Common
{
    public class ApiException
    {


        public ApiException(int statusCode, string message, string details = null)
        {
            StatusCode = statusCode;
            Message = message;
            Details = details;
        }

        public int StatusCode { get; private set; }
        public string Message { get; private set; }
        public string Details { get; private set; }

    }
}
