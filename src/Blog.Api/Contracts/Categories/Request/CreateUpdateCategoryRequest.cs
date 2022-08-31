using System.ComponentModel.DataAnnotations;

namespace Blog.Api.Contracts.Categories.Request
{
    public class CreateUpdateCategoryRequest
    {
        [Required(ErrorMessage = "A descrição da categoria deve ser informada")]
        [MinLength(4, ErrorMessage = "A descrição deve ter no mínimo 4 caracteres")]
        public string Description { get; set; }
    }
}
