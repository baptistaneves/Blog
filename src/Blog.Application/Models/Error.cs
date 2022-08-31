using Blog.Application.Enums;

namespace Blog.Application.Models
{
    public class Error
    {
        public ErrorCode Code { get; set; }
        public string Message { get; set; }
    }
}
