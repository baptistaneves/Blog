using Blog.Application.Enums;
using Blog.Application.Models;
using Blog.Application.Posts.Queries;
using Blog.Dal.Context;
using Blog.Domain.Aggregates.PostAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blog.Application.Posts.QueryHandlers
{
    public class GetAllPostReactionsQueryHandler : IRequestHandler<GetAllPostReactionsQuery, 
        OperationResult<IEnumerable<PostReaction>>>
    {
        private readonly DataContext _context;
        private readonly OperationResult<IEnumerable<PostReaction>> _result;
        public GetAllPostReactionsQueryHandler(DataContext context)
        {
            _context = context;
            _result = new OperationResult<IEnumerable<PostReaction>>();
        }

        public async Task<OperationResult<IEnumerable<PostReaction>>> Handle(GetAllPostReactionsQuery request, 
            CancellationToken cancellationToken)
        {
            try
            {
                var post = await _context.Posts.AsNoTracking()
                    .Include(p => p.Reactions)
                    .ThenInclude(pc => pc.UserProfile)
                    .FirstOrDefaultAsync(p => p.PostId == request.PostId, cancellationToken);

                if (post is null)
                {
                    _result.AddError(ErrorCode.NotFound, PostErrorMessages.PostNotFound);
                    return _result;
                }

                _result.Payload = post.Reactions.ToList();
            }
            catch (Exception ex)
            {
                _result.AddUnknownError($"{ex.Message}");
            }

            return _result;
        }
    }
}
