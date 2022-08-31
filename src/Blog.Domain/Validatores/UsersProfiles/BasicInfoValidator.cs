using Blog.Domain.Aggregates.UserProfileAggregate;
using FluentValidation;

namespace Blog.Domain.Validatores.UsersProfiles
{
    internal class BasicInfoValidator : AbstractValidator<BasicInfo>
    {
        public BasicInfoValidator()
        {
            RuleFor(bi => bi.FirstName)
                .NotEmpty().WithMessage("O primeiro nome deve ser informado")
                .MinimumLength(3).WithMessage("O primeiro nome deve ter no mínimo 5 caracteres");

            RuleFor(bi => bi.LastName)
                .NotEmpty().WithMessage("O último nome deve ser informado")
                 .MinimumLength(3).WithMessage("O último nome deve ter no mínimo 5 caracteres");

            RuleFor(bi => bi.EmailAddress)
                .NotEmpty().WithMessage("O e-mail deve ser informado")
                .EmailAddress().WithMessage("O e-mail informado não é válido");

            RuleFor(bi => bi.Phone)
                .NotEmpty().WithMessage("O telefone deve ser informado");
        }
    }
}
