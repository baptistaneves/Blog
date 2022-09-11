using Blog.Application.Enums;
using Blog.Application.Models;
using Blog.Application.Posts.Commands;
using Blog.Dal.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blog.Application.Posts.CommandHandlers
{
    public class RemoveCommentAnswerCommandHandler : IRequestHandler<RemoveCommentAnswerCommand,
        OperationResult<bool>>
    {
        private readonly DataContext _context;
        private readonly OperationResult<bool> _result;
        public RemoveCommentAnswerCommandHandler(DataContext context)
        {
            _context = context;
            _result = new OperationResult<bool>();
        }

        public async Task<OperationResult<bool>> Handle(RemoveCommentAnswerCommand request, 
            CancellationToken cancellationToken)
        {
            try
            {
                var post = await _context.Posts.AsNoTracking()
                    .Include(p => p.Comments)
                    .ThenInclude(pc=> pc.Answers)
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

                var commentAnswer = postComment.Answers.FirstOrDefault(ca => ca.CommentAnswerId == request.CommentAnswerId);
                if (commentAnswer is null)
                {
                    _result.AddError(ErrorCode.NotFound, PostErrorMessages.CommentAnswerNotFound);
                    return _result;
                }

                postComment.RemoveCommentAnswer(commentAnswer);

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
