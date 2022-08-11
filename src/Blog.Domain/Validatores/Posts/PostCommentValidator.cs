using Blog.Domain.Aggregates.PostAggregate;
using FluentValidation;

namespace Blog.Domain.Validatores.Posts
{
    internal class PostCommentValidator : AbstractValidator<PostComment>
    {
        public PostCommentValidator()
        {
            RuleFor(pc => pc.Text)
                .NotEmpty().WithMessage("Nengum comentário foi adicionado");
        }
    }
}
