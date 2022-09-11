using Blog.Application.Enums;
using Blog.Application.Models;
using Blog.Application.Posts.Commands;
using Blog.Dal.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Posts.CommandHandlers
{
    public class RemovePostReactionCommandHandler : IRequestHandler<RemovePostReactionCommand, 
        OperationResult<bool>>
    {
        private readonly DataContext _context;
        private readonly OperationResult<bool> _result;
        public RemovePostReactionCommandHandler(DataContext context)
        {
            _context = context;
            _result = new OperationResult<bool>();
        }

        public async Task<OperationResult<bool>> Handle(RemovePostReactionCommand request, 
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

                var postReaction = post.Reactions.FirstOrDefault(pr => pr.PostReactionId == request.PostReactionId);
                if (postReaction is null)
                {
                    _result.AddError(ErrorCode.NotFound, PostErrorMessages.PostReactionNotFound);
                    return _result;
                }

                post.RemovePostReaction(postReaction);

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
