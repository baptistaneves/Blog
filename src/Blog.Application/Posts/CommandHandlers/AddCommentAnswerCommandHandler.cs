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
    public class AddCommentAnswerCommandHandler : IRequestHandler<AddCommentAnswerCommand, 
        OperationResult<CommentAnswer>>
    {
        private readonly DataContext _context;
        private readonly OperationResult<CommentAnswer> _result;
        public AddCommentAnswerCommandHandler(DataContext context)
        {
            _context = context;
            _result = new OperationResult<CommentAnswer>();
        }

        public async Task<OperationResult<CommentAnswer>> Handle(AddCommentAnswerCommand request, 
            CancellationToken cancellationToken)
        {
            try
            {
                var post = await _context.Posts.AsNoTracking()
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

                var commentAnswer = CommentAnswer.CreateCommentAnswer(request.UserProfileId, request.CommentId, request.Text);
                
                postComment.AddCommetAnswer(commentAnswer);
                post.AddPostComment(postComment);

                _context.Posts.Update(post);
                await _context.SaveChangesAsync(cancellationToken);

                _result.Payload = commentAnswer;
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
