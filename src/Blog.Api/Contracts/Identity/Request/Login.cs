using System.ComponentModel.DataAnnotations;

namespace Blog.Api.Contracts.Identity.Request
{
    public class Login
    {
        [Required(ErrorMessage = "O nome de usuário deve ser informado")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "A senha deve ser informada")]
        public string Password { get; set; }
    }
}
