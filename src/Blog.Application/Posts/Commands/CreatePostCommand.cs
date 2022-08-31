using Blog.Application.Models;
using Blog.Domain.Aggregates.PostAggregate;
using MediatR;

namespace Blog.Application.Posts.Commands
{
    public class CreatePostCommand : IRequest<OperationResult<Post>>
    {
        public Guid UserProfileId { get; set; }
        public Guid CategoryId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
    }
}
