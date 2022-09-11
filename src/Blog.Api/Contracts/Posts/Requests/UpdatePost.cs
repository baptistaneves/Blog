using System.ComponentModel.DataAnnotations;

namespace Blog.Api.Contracts.Posts.Requests
{
    public class UpdatePost
    {
        [Required(ErrorMessage = "A categoria da notícia deve ser informada")]
        public Guid CategoryId { get; set; }

        [Required(ErrorMessage = "O titulo da notícia deve ser informado")]
        public string Title { get; set; }

        [Required(ErrorMessage = "A notícia não pode ter um conteúdo vázio")]
        public string Content { get; set; }

        public IFormFile Image { get; set; }
    }
}
