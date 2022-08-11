using Blog.Domain.Entities.Categories;
using FluentValidation;

namespace Blog.Domain.Validatores.Categories
{
    internal class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(c => c.Description)
                .NotEmpty().WithMessage("A descrição da categoria deve ser informada")
                .MinimumLength(5).WithMessage("A descrição deve ter no mínimo 5 caracteres");
        }
    }
}
