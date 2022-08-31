using Blog.Application.Enums;
using Blog.Application.Models;
using Blog.Application.Posts.Dtos;
using Blog.Application.Posts.Queries;
using Blog.Dal.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blog.Application.Posts.QueryHandlers
{
    public class GetPostByIdQueryHandler : IRequestHandler<GetPostByIdQuery, OperationResult<PostDto>>
    {
        private readonly DataContext _context;
        private readonly OperationResult<PostDto> _result;
        public GetPostByIdQueryHandler(DataContext context)
        {
            _context = context;
            _result = new OperationResult<PostDto>();
        }

        public async Task<OperationResult<PostDto>> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var post = await _context.Posts.AsNoTracking()
                                        .Include(p => p.Category)
                                        .Include(p => p.UserProfile)
                                        .Select(p => new PostDto
                                        {
                                            PostId = p.PostId,
                                            UserProfileId = p.UserProfileId,
                                            Title = p.Title,
                                            Content = p.Content,
                                            CategoryId = p.CategoryId,
                                            Image = p.Image,
                                            CategoryDescritpion = p.Category.Description,
                                            CreatedBy = p.UserProfile.BasicInfo.FirstName + " " + p.UserProfile.BasicInfo.LastName,
                                            CreatedAt = p.CreatedAt,
                                            LastModified = p.LastModified
                                        }).FirstOrDefaultAsync(p=> p.PostId == request.PostId);

                if(post is null)
                {
                    _result.AddError(ErrorCode.NotFound, PostErrorMessages.PostNotFound);
                    return _result;
                }
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
