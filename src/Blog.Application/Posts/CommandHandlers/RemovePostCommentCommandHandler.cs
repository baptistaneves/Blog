using Blog.Application.Enums;
using Blog.Application.Models;
using Blog.Application.Posts.Commands;
using Blog.Dal.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blog.Application.Posts.CommandHandlers
{
    public class RemovePostCommentCommandHandler : IRequestHandler<RemovePostCommentCommand, OperationResult<bool>>
    {
        private readonly DataContext _context;
        private readonly OperationResult<bool> _result;
        public RemovePostCommentCommandHandler(DataContext context)
        {
            _context = context;
            _result = new OperationResult<bool>();
        }

        public async Task<OperationResult<bool>> Handle(RemovePostCommentCommand request,
            CancellationToken cancellationToken)
        {

            try
            {
                var post = await _context.Posts.AsNoTracking()
                    .Include(pc => pc.Comments)
                    .FirstOrDefaultAsync(p => p.PostId == request.PostId, cancellationToken);

                if (post is null)
                {
                    _result.AddError(ErrorCode.NotFound, PostErrorMessages.PostNotFound);
                    return _result;
                }

                var postComment = post.Comments.FirstOrDefault(pc => pc.PostCommentId == request.PostCommentId);
                if (postComment is null)
                {
                    _result.AddError(ErrorCode.NotFound, PostErrorMessages.PostCommentNotFound);
                    return _result;
                }

                post.RemovePostComment(postComment);

                _context.Posts.Update(post);
                await _context.SaveChangesAsync(cancellationToken);

                _result.Payload = true;
            }
            catch (Exception ex)
            {
                _result.AddUnknownError($"{ex.Message}");
            }

            return _result;
        }
    }
}
