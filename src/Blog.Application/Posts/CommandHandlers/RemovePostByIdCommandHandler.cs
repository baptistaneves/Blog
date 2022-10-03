using Blog.Application.Enums;
using Blog.Application.Models;
using Blog.Application.Posts.Commands;
using Blog.Dal.Context;
using Blog.Domain.Aggregates.PostAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blog.Application.Posts.CommandHandlers
{
    public class RemovePostByIdCommandHandler : IRequestHandler<RemovePostByIdCommand, OperationResult<Post>>
    {
        private readonly DataContext _context;
        private readonly OperationResult<Post> _result;
        public RemovePostByIdCommandHandler(DataContext context)
        {
            _context = context;
            _result = new OperationResult<Post>();
        }

        public async Task<OperationResult<Post>> Handle(RemovePostByIdCommand request, 
            CancellationToken cancellationToken)
        {
            try
            {
                var post = await _context.Posts.AsNoTracking()
                    .FirstOrDefaultAsync(p=> p.PostId == request.PostId, cancellationToken);

                if (post is null)
                {
                    _result.AddError(ErrorCode.NotFound, PostErrorMessages.PostNotFound);
                    return _result;
                }

                _context.Posts.Remove(post);
                await _context.SaveChangesAsync(cancellationToken);

                _result.Payload = post;
            }
            catch (Exception ex)
            {
                _result.AddUnknownError($"{ex.Message}");
            }

            return _result;
        }
    }
}
