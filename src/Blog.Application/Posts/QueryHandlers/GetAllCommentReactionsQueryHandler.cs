using Blog.Application.Enums;
using Blog.Application.Models;
using Blog.Application.Posts.Queries;
using Blog.Dal.Context;
using Blog.Domain.Aggregates.PostAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blog.Application.Posts.QueryHandlers
{
    public class GetAllCommentReactionsQueryHandler : IRequestHandler<GetAllCommentReactionsQuery, 
        OperationResult<IEnumerable<CommentReaction>>>
    {
        private readonly DataContext _context;
        private readonly OperationResult<IEnumerable<CommentReaction>> _result;
        public GetAllCommentReactionsQueryHandler(DataContext context)
        {
            _context = context;
            _result = new OperationResult<IEnumerable<CommentReaction>>();
        }

        public async Task<OperationResult<IEnumerable<CommentReaction>>> Handle(GetAllCommentReactionsQuery request, 
            CancellationToken cancellationToken)
        {
            try
            {
                var post = await _context.Posts.AsNoTracking()
                    .Include(p => p.Comments)
                    .ThenInclude(pc => pc.Reactions)
                    .ThenInclude(ca => ca.UserProfile)
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

                _result.Payload = postComment.Reactions.ToList();
            }
            catch (Exception ex)
            {
                _result.AddUnknownError($"{ex.Message}");
            }

            return _result;
        }
    }
}
