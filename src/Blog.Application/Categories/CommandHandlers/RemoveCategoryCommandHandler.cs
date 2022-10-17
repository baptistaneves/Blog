using Blog.Application.Categories.Commands;
using Blog.Application.Enums;
using Blog.Application.Models;
using Blog.Dal.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blog.Application.Categories.CommandHandlers
{
    public class RemoveCategoryCommandHandler : IRequestHandler<RemoveCategoryCommand, OperationResult<bool>>
    {
        private readonly DataContext _context;
        private readonly OperationResult<bool> _result;

        public RemoveCategoryCommandHandler(DataContext context)
        {
            _context = context;
            _result = new OperationResult<bool>();
        }

        public async Task<OperationResult<bool>> Handle(RemoveCategoryCommand request, 
            CancellationToken cancellationToken)
        {
            try
            {
                var category = await _context.Categories.AsNoTracking()
                    .Include(c=> c.Posts)
                    .FirstOrDefaultAsync(c=> c.CategoryId == request.CategoryId, cancellationToken);

                if(category is null)
                {
                    _result.AddError(ErrorCode.NotFound, CategoryErrorMessages.CategoryNotFound);
                    return _result;
                }

                if(category.Posts.Any())
                {
                    _result.AddError(ErrorCode.CategoryHasPosts, CategoryErrorMessages.CategoryRemovalNotAuthorized);
                    return _result;
                }

                _context.Remove(category);
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
