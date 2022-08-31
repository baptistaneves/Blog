using Blog.Application.Models;
using MediatR;

namespace Blog.Application.Categories.Commands
{
    public class UpdateCategoryCommand : IRequest<OperationResult<bool>>
    {
        public Guid CategoryId { get; set; }
        public string Description { get; set; }
    }
}
