using Blog.Application.Enums;
using Blog.Application.Models;
using Blog.Application.Posts.Queries;
using Blog.Dal.Context;
using Blog.Domain.Aggregates.PostAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blog.Application.Posts.QueryHandlers
{
    public class GetPostCommentByIdQueryHandler : IRequestHandler<GetPostCommentByIdQuery, 
        OperationResult<PostComment>>
    {
        private readonly DataContext _context;
        private readonly OperationResult<PostComment> _result;
        public GetPostCommentByIdQueryHandler(DataContext context)
        {
            _context = context;
            _result = new OperationResult<PostComment>();
        }

        public async Task<OperationResult<PostComment>> Handle(GetPostCommentByIdQuery request, 
            CancellationToken cancellationToken)
        {
            try
            {
                var post = await _context.Posts.AsNoTracking()
                    .Include(p => p.Comments)
                    .ThenInclude(pc => pc.UserProfile)
                    .FirstOrDefaultAsync(p => p.PostId == request.PostId, cancellationToken);

                if (post is null)
                {
                    _result.AddError(ErrorCode.NotFound, PostErrorMessages.PostNotFound);
                    return _result;
                }

                var postComment = post.Comments.FirstOrDefault(pc=> pc.PostCommentId == request.PostCommentId);

                if (postComment is null)
                {
                    _result.AddError(ErrorCode.NotFound, PostErrorMessages.PostCommentNotFound);
                    return _result;
                }

                _result.Payload = postComment;
            }
            catch (Exception ex)
            {
                _result.AddUnknownError($"{ex.Message}");
            }

            return _result;
        }
    }
}
