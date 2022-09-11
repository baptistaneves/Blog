using Blog.Application.Enums;
using Blog.Application.Models;
using Blog.Application.Posts.Commands;
using Blog.Dal.Context;
using Blog.Domain.Exceptions.Posts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blog.Application.Posts.CommandHandlers
{
    public class UpdateCommentAnswerCommandHandler : IRequestHandler<UpdateCommentAnswerCommand, OperationResult<bool>>
    {
        private readonly DataContext _context;
        private readonly OperationResult<bool> _result;
        public UpdateCommentAnswerCommandHandler(DataContext context)
        {
            _context = context;
            _result = new OperationResult<bool>();
        }

        public async Task<OperationResult<bool>> Handle(UpdateCommentAnswerCommand request, 
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

                var postComment = post.Comments.FirstOrDefault(pc => pc.PostCommentId == request.CommentId);
                if (postComment is null)
                {
                    _result.AddError(ErrorCode.NotFound, PostErrorMessages.PostCommentNotFound);
                    return _result;
                }

                postComment.UpdateCommentAnswer(request.CommentAnswerId, request.UpdatedText);

                _context.Posts.Update(post);
                await _context.SaveChangesAsync(cancellationToken);

                _result.Payload = true;
            }
            catch (CommentAnswerNotValidException ex)
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
