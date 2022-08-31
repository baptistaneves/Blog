namespace Blog.Api.Contracts.Common
{
    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string StatusPhrase { get; set; }
        public DateTime TimeStamp { get; set; }
        public List<string> Errors { get; } = new List<string>();
    }
}
