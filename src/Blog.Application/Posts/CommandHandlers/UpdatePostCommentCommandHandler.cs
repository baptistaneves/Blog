using Blog.Application.Enums;
using Blog.Application.Models;
using Blog.Application.Posts.Commands;
using Blog.Dal.Context;
using Blog.Domain.Exceptions.Posts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blog.Application.Posts.CommandHandlers
{
    public class UpdatePostCommentCommandHandler : IRequestHandler<UpdatePostCommentCommand, OperationResult<bool>>
    {
        private readonly DataContext _context;
        private readonly OperationResult<bool> _result;
        public UpdatePostCommentCommandHandler(DataContext context)
        {
            _context = context;
            _result = new OperationResult<bool>();
        }

        public async Task<OperationResult<bool>> Handle(UpdatePostCommentCommand request, 
            CancellationToken cancellationToken)
        {
            try
            {
                var post = await _context.Posts.AsNoTracking()
                    .Include(pc=> pc.Comments)
                    .FirstOrDefaultAsync(p => p.PostId == request.PostId, cancellationToken);

                if (post is null)
                {
                    _result.AddError(ErrorCode.NotFound, PostErrorMessages.PostNotFound);
                    return _result;
                }

                post.UpdatePostComment(request.PostCommentId, request.Text);

                _context.Posts.Update(post);
                await _context.SaveChangesAsync(cancellationToken);

                _result.Payload = true;
            }
            catch (PostCommentNotValidException ex)
            {
                ex.ValidationErrors.ForEach(error => _result.AddError(ErrorCode.ValidationErrors, error));
            }
            catch (Exception ex)
            {
                _result.AddUnknownError($"{ex.Message}");
            }

            return _result;
        }
    }
}
