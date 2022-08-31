using System.ComponentModel.DataAnnotations;

namespace Blog.Api.Contracts.Identity.Request
{
    public class UserRegistrationRequest
    {
        [Required(ErrorMessage = "O primeiro nome deve ser informado")]
        [MinLength(3, ErrorMessage = "O nome deve ter no mínimo 3 caracteres")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "O último nome deve ser informado")]
        [MinLength(3, ErrorMessage = "O nome deve ter no mínimo 3 caracteres")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "O e-mail deve ser informado")]
        [EmailAddress(ErrorMessage = "O e-mail informado não é válido")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "A senha deve ser informada")]
        public string Password { get; set; }

        public string Phone { get; set; }
    }
}
