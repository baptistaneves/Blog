using Blog.Application.Enums;
using Blog.Application.Models;
using Blog.Application.Posts.Commands;
using Blog.Dal.Context;
using Blog.Domain.Aggregates.PostAggregate;
using Blog.Domain.Exceptions.Posts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blog.Application.Posts.CommandHandlers
{
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, OperationResult<Post>>
    {
        private readonly DataContext _context;
        private readonly OperationResult<Post> _result;
        public CreatePostCommandHandler(DataContext context)
        {
            _context = context;
            _result = new OperationResult<Post>();
        }

        public async Task<OperationResult<Post>> Handle(CreatePostCommand request, 
            CancellationToken cancellationToken)
        {
            try
            {
                await PostTitleAlreadyExists(request.Title, _result, cancellationToken);
                if(_result.IsError) return _result;

                var post = Post.CreatePost(request.UserProfileId, request.CategoryId, 
                    request.Title, request.Content, request.Image);

                _context.Posts.Add(post);
                await _context.SaveChangesAsync(cancellationToken);

                _result.Payload = post;
            }
            catch (PostNotValidException ex)
            {
                ex.ValidationErrors.ForEach(error => _result.AddError(ErrorCode.ValidationErrors, error));
            }
            catch (Exception ex)
            {
                _result.AddUnknownError($"{ex}");
            }

            return _result;
        }

        private async Task PostTitleAlreadyExists(string title, OperationResult<Post> result, CancellationToken token)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(p => p.Title == title, token);
            if(post != null)
            {
                result.AddError(ErrorCode.PostTitleAlreadyExists, PostErrorMessages.PostTitleAlreadyExists);
            }
        }
    }
}
