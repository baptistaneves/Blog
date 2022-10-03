using Blog.Application.Models;
using Blog.Application.Posts.Dtos;
using Blog.Application.Posts.Queries;
using Blog.Dal.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blog.Application.Posts.QueryHandlers
{
    public class GetAllPostsQueryHandler : IRequestHandler<GetAllPostsQuery, OperationResult<PagedList<PostDto>>>
    {
        private readonly DataContext _context;
        private readonly OperationResult<PagedList<PostDto>> _result;
        public GetAllPostsQueryHandler(DataContext context)
        {
            _context = context;
            _result = new OperationResult<PagedList<PostDto>>();
        }

        public async Task<OperationResult<PagedList<PostDto>>> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var items =  _context.Posts.AsNoTracking()
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
                                        }).OrderBy(p=> p.CreatedAt.Date)
                                          .AsQueryable();

                _result.Payload = await PagedList<PostDto>
                    .CreateAsync(items, request.Params.CurrentPage, request.Params.ItemsPerPage);
            }
            catch (Exception ex)
            {
                _result.AddUnknownError($"{ex.Message}");
            }

            return _result;
        }
    }
}
