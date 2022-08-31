using Blog.Application.Enums;
using Blog.Application.Models;
using Blog.Application.Posts.Commands;
using Blog.Dal.Context;
using Blog.Domain.Exceptions.Posts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blog.Application.Posts.CommandHandlers
{
    public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand, OperationResult<bool>>
    {
        private readonly DataContext _context;
        private readonly OperationResult<bool> _result;
        public UpdatePostCommandHandler(DataContext context)
        {
            _context = context;
            _result = new OperationResult<bool>();
        }

        public async Task<OperationResult<bool>> Handle(UpdatePostCommand request, 
            CancellationToken cancellationToken)
        {
            try
            {
                await PostTitleAlreadyExists(request.Title, request.PostId, _result, cancellationToken);
                if (_result.IsError) return _result;

                var post = await _context.Posts.FirstOrDefaultAsync(p => p.PostId == request.PostId, cancellationToken);
                if (post is null)
                {
                    _result.AddError(ErrorCode.NotFound, PostErrorMessages.PostNotFound);
                    return _result;
                }

                request.Image = String.IsNullOrWhiteSpace(request.Image) ? post.Image : request.Image;

                post.UpdatePost(request.CategoryId, request.Title, request.Content, request.Image);

                _context.Posts.Update(post);
                await _context.SaveChangesAsync(cancellationToken);

                _result.Payload = true;
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

        private async Task PostTitleAlreadyExists(string title, Guid postId,
                     OperationResult<bool> result, CancellationToken token)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(p => p.Title == title && p.PostId != postId, token);
            if (post != null)
            {
                result.AddError(ErrorCode.PostTitleAlreadyExists, PostErrorMessages.PostTitleAlreadyExists);
            }
        }
    }
}
