using Blog.Application.Models;
using Blog.Domain.Entities.Categories;
using MediatR;

namespace Blog.Application.Categories.Commands
{
    public class AddCategoryCommand : IRequest<OperationResult<Category>>
    {
        public string Description { get; set; }
    }
}
