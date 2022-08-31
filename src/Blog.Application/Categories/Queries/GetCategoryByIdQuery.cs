using Blog.Application.Models;
using Blog.Domain.Entities.Categories;
using MediatR;

namespace Blog.Application.Categories.Queries
{
    public class GetCategoryByIdQuery : IRequest<OperationResult<Category>>
    {
        public Guid CategoryId { get; set; }
    }
}
