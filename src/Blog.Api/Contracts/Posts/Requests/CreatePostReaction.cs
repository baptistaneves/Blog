using Blog.Domain.Aggregates.PostAggregate;
using System.ComponentModel.DataAnnotations;

namespace Blog.Api.Contracts.Posts.Requests
{
    public class CreatePostReaction
    {
        [Required(ErrorMessage = "Informe o tipo de reação")]
        public ReactionType ReactionType { get; set; }
    }
}
