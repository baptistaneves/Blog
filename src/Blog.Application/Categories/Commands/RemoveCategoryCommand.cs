using Blog.Application.Models;
using MediatR;

namespace Blog.Application.Categories.Commands
{
    public class RemoveCategoryCommand : IRequest<OperationResult<bool>>
    {
        public Guid CategoryId { get; set; }
    }
}
