using Blog.Application.Enums;
using Blog.Application.Models;
using Blog.Application.Posts.Commands;
using Blog.Dal.Context;
using Blog.Domain.Aggregates.PostAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Posts.CommandHandlers
{
    public class AddCommentReactionCommandHandler : IRequestHandler<AddCommentReactionCommand, 
        OperationResult<CommentReaction>>
    {
        private readonly DataContext _context;
        private readonly OperationResult<CommentReaction> _result;
        public AddCommentReactionCommandHandler(DataContext context)
        {
            _context = context;
            _result = new OperationResult<CommentReaction>();
        }

        public async Task<OperationResult<CommentReaction>> Handle(AddCommentReactionCommand request, 
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

                var postComment = post.Comments.FirstOrDefault(pc => pc.PostCommentId == request.PostCommentId);
                if (postComment is null)
                {
                    _result.AddError(ErrorCode.NotFound, PostErrorMessages.PostCommentNotFound);
                    return _result;
                }

                var commentReaction = CommentReaction
                    .CreateCommentReaction(request.UserProfileId, request.PostCommentId, request.ReactionType);

                postComment.AddReactionComment(commentReaction);
                post.AddPostComment(postComment);

                _context.Posts.Update(post);
                await _context.SaveChangesAsync(cancellationToken);

                _result.Payload = commentReaction;
            }
            catch (Exception ex)
            {
                _result.AddUnknownError($"{ex.Message}");
            }

            return _result;
        }
    }
}
