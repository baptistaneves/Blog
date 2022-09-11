using System.ComponentModel.DataAnnotations;

namespace Blog.Api.Contracts.Posts.Requests
{
    public class CreateUpdatePostComment
    {
        [Required(ErrorMessage = "O comentário não deve estar vázio")]
        public string Text { get; set; }
    }
}
