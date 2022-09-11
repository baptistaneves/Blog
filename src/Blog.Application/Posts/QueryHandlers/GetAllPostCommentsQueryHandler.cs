using Blog.Application.Enums;
using Blog.Application.Models;
using Blog.Application.Posts.Dtos;
using Blog.Application.Posts.Queries;
using Blog.Dal.Context;
using Blog.Domain.Aggregates.PostAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blog.Application.Posts.QueryHandlers
{
    public class GetAllPostCommentsQueryHandler : IRequestHandler<GetAllPostCommentsQuery, 
        OperationResult<IEnumerable<PostComment>>>
    {
        private readonly DataContext _context;
        private OperationResult<IEnumerable<PostComment>> _result;
        public GetAllPostCommentsQueryHandler(DataContext context)
        {
            _context = context;
            _result = new OperationResult<IEnumerable<PostComment>>();
        }

        public async Task<OperationResult<IEnumerable<PostComment>>> Handle(GetAllPostCommentsQuery request, 
            CancellationToken cancellationToken)
        {
            try
            {
                var post = await _context.Posts.AsNoTracking()
                    .Include(p=> p.Comments)
                    .ThenInclude(pc=> pc.UserProfile)
                    .FirstOrDefaultAsync(p => p.PostId == request.PostId, cancellationToken);

                if (post is null)
                {
                    _result.AddError(ErrorCode.NotFound, PostErrorMessages.PostNotFound);
                    return _result;
                }

                _result.Payload = post.Comments.ToList();
            }
            catch (Exception ex)
            {
                _result.AddUnknownError($"{ex.Message}");
            }

            return _result;
        }
    }
}
