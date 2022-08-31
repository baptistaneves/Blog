using Blog.Application.Categories.Commands;
using Blog.Application.Enums;
using Blog.Application.Models;
using Blog.Dal.Context;
using Blog.Domain.Entities.Categories;
using Blog.Domain.Exceptions.Categories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blog.Application.Categories.CommandHandlers
{
    public class AddCategoryCommandHandler : IRequestHandler<AddCategoryCommand, OperationResult<Category>>
    {
        private readonly DataContext _context;
        private readonly OperationResult<Category> _result;
        public AddCategoryCommandHandler(DataContext context)
        {
            _context = context;
            _result = new OperationResult<Category>();
        }

        public async Task<OperationResult<Category>> Handle(AddCategoryCommand request, 
            CancellationToken cancellationToken)
        {
            try
            {
                var categoryExists = await _context.Categories
                    .FirstOrDefaultAsync(c=> c.Description == request.Description, cancellationToken);

                if (categoryExists != null)
                {
                    _result.AddError(ErrorCode.CategoryAlreadyExists, CategoryErrorMessages.CategoryAlreadyExists);
                    return _result;
                }

                var category = Category.CreateCategory(request.Description);

                _context.Categories.Add(category);
                await _context.SaveChangesAsync(cancellationToken); 
                
                _result.Payload = category;
            }
            catch (CategoryNotValidException ex)
            {
                ex.ValidationErrors.ForEach(error =>
                {
                    _result.AddError(ErrorCode.ValidationErrors, error);
                });
            }
            catch (Exception ex)
            {
                _result.AddUnknownError($"{ex.Message}");
            }

            return _result;
        }
    }
}
