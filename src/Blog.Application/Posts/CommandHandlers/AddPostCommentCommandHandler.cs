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
    public class AddPostCommentCommandHandler : IRequestHandler<AddPostCommentCommand, 
        OperationResult<PostComment>>
    {
        private readonly DataContext _context;
        private readonly OperationResult<PostComment> _result;
        public AddPostCommentCommandHandler(DataContext context)
        {
            _context = context;
            _result = new OperationResult<PostComment>();
        }

        public async Task<OperationResult<PostComment>> Handle(AddPostCommentCommand request, 
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

                var postComment = PostComment.CreatePostComment(request.PostId, request.UserProfileId, request.Text);
                post.AddPostComment(postComment);

                _context.Posts.Update(post);
                await _context.SaveChangesAsync(cancellationToken);

                _result.Payload = postComment;
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
