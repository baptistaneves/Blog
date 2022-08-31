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
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, OperationResult<bool>>
    {
        private readonly DataContext _context;
        private readonly OperationResult<bool> _result;
        public UpdateCategoryCommandHandler(DataContext context)
        {
            _context = context;
            _result = new OperationResult<bool>();
        }

        public async Task<OperationResult<bool>> Handle(UpdateCategoryCommand request, 
            CancellationToken cancellationToken)
        {
            try
            {
                await ValidateCategoryAlreadyExists(_result, request, cancellationToken);
                if (_result.IsError) return _result;

                var category = await _context.Categories.AsNoTracking()
                    .FirstOrDefaultAsync(c=> c.CategoryId == request.CategoryId, cancellationToken);

                if (category is null)
                {
                    _result.AddError(ErrorCode.NotFound, CategoryErrorMessages.CategoryNotFound);
                    return _result;
                }

                category.UpdateCategory(request.Description);

                _context.Categories.Update(category);
                await _context.SaveChangesAsync(cancellationToken);

                _result.Payload = true;
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

        private async Task ValidateCategoryAlreadyExists(OperationResult<bool> result, 
            UpdateCategoryCommand request, CancellationToken token)
        {
            var category = await _context.Categories
                    .FirstOrDefaultAsync(c => c.Description == request.Description
                        && c.CategoryId != request.CategoryId, token);

            if (category != null)
            {
                result.AddError(ErrorCode.CategoryAlreadyExists, CategoryErrorMessages.CategoryAlreadyExists);
            }
        }
    }
}
