using Blog.Domain.Aggregates.PostAggregate;
using FluentValidation;

namespace Blog.Domain.Validatores.Posts
{
    internal class PostCreateValidator : AbstractValidator<Post>
    {
        public PostCreateValidator()
        {
            RuleFor(p => p.Category)
                .NotEmpty().WithMessage("A categoria da notícia deve ser informada");

            RuleFor(p => p.Title)
               .NotEmpty().WithMessage("O titulo da notícia deve ser informado");

            RuleFor(p => p.Content)
               .NotEmpty().WithMessage("Não foi criado nenhum conteúdo para a notícia");

            RuleFor(p => p.Image)
               .NotEmpty().WithMessage("Selecione uma imagem para a notícia");
        }
    }
}
