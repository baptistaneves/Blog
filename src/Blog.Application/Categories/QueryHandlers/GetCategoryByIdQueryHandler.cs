using Blog.Application.Categories.Queries;
using Blog.Application.Enums;
using Blog.Application.Models;
using Blog.Dal.Context;
using Blog.Domain.Entities.Categories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blog.Application.Categories.QueryHandlers
{
    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, OperationResult<Category>>
    {
        private readonly DataContext _context;
        private readonly OperationResult<Category> _result;

        public GetCategoryByIdQueryHandler(DataContext context)
        {
            _context = context;
            _result = new OperationResult<Category>();
        }

        public async Task<OperationResult<Category>> Handle(GetCategoryByIdQuery request, 
            CancellationToken cancellationToken)
        {
            try
            {
                var category = await _context.Categories.AsNoTracking()
                    .FirstOrDefaultAsync(c => c.CategoryId == request.CategoryId, cancellationToken);

                if(category is null)
                {
                    _result.AddError(ErrorCode.NotFound, CategoryErrorMessages.CategoryNotFound);
                    return _result;
                }

                _result.Payload = category;
            }
            catch (Exception ex)
            {
                _result.AddUnknownError($"{ex.Message}");
            }

            return _result;
        }
    }
}
