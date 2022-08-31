using Blog.Application.Enums;

namespace Blog.Application.Models
{
    public class OperationResult<T>
    {
        public T Payload { get; set; }
        public bool IsError { get; private set; }
        public List<Error> Errors { get; } = new List<Error>();

        public void AddError(ErrorCode code, string message)
        {
            HandleError(code, message);
        }

        public void AddUnknownError(string message)
        {
            Errors.Add(new Error { Code = ErrorCode.UnknownError, Message = message });
        }

        private void HandleError(ErrorCode code, string message)
        {
            IsError = true;
            Errors.Add(new Error { Code = code, Message = message });
        }

        public void ResetIsErrorFlag()
        {
            IsError = false;
        }
    }
}
