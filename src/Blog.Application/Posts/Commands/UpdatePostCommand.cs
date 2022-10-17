using Blog.Application.Models;
using MediatR;

namespace Blog.Application.Posts.Commands
{
    public class UpdatePostCommand : IRequest<OperationResult<string>>
    {
        public Guid PostId { get; set; }
        public Guid CategoryId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
    }
}
