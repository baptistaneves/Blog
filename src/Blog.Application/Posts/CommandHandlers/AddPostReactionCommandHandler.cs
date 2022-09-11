using Blog.Application.Enums;
using Blog.Application.Models;
using Blog.Application.Posts.Commands;
using Blog.Dal.Context;
using Blog.Domain.Aggregates.PostAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blog.Application.Posts.CommandHandlers
{
    public class AddPostReactionCommandHandler : IRequestHandler<AddPostReactionCommand, OperationResult<PostReaction>>
    {
        private readonly DataContext _context;
        private readonly OperationResult<PostReaction> _result;
        public AddPostReactionCommandHandler(DataContext context)
        {
            _context = context;
            _result = new OperationResult<PostReaction>();
        }

        public async Task<OperationResult<PostReaction>> Handle(AddPostReactionCommand request, 
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

                var postReaction = PostReaction.CreatePostReaction(request.PostId, request.UserProfileId, request.ReactionType);
                post.AddPostReaction(postReaction);

                _context.Posts.Update(post);
                await _context.SaveChangesAsync(cancellationToken);

                _result.Payload = postReaction;
            }
            catch (Exception ex)
            {
                _result.AddUnknownError($"{ex.Message}");
            }

            return _result;
        }
    }
}
