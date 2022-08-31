using Blog.Application.Categories.Queries;
using Blog.Application.Models;
using Blog.Dal.Context;
using Blog.Domain.Entities.Categories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blog.Application.Categories.QueryHandlers
{
    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, 
        OperationResult<IEnumerable<Category>>>
    {
        private readonly DataContext _context;
        private readonly OperationResult<IEnumerable<Category>> _result;

        public GetAllCategoriesQueryHandler(DataContext context)
        {
            _context = context;
            _result = new OperationResult<IEnumerable<Category>>();
        }

        public async Task<OperationResult<IEnumerable<Category>>> Handle(GetAllCategoriesQuery request, 
            CancellationToken cancellationToken)
        {
            try
            {
                _result.Payload = await _context.Categories.AsNoTracking().ToListAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _result.AddUnknownError($"{ex.Message}");
            }

            return _result;
        }
    }
}
