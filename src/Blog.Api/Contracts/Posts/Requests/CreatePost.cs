using System.ComponentModel.DataAnnotations;

namespace Blog.Api.Contracts.Posts.Requests
{
    public class CreatePost
    {
        [Required(ErrorMessage = "A categoria da notícia deve ser informada")]
        public Guid CategoryId { get; set; }

        [Required(ErrorMessage = "O titulo da notícia deve ser informado")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Não foi criado nenhum conteúdo para esta notícia")]
        public string Content { get; set; }

        [Required(ErrorMessage = "Selecione uma imagem para esta notícia")]
        public IFormFile Image { get; set; }
    }
}
