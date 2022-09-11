using Blog.Domain.Aggregates.PostAggregate;
using FluentValidation;

namespace Blog.Domain.Validatores.Posts
{
    internal class PostCommentResponseValidator : AbstractValidator<CommentAnswer>
    {
        public PostCommentResponseValidator()
        {
            RuleFor(cr => cr.Text)
                .NotEmpty().WithMessage("Nenhum texto foi adicionado");
        }
    }
}
