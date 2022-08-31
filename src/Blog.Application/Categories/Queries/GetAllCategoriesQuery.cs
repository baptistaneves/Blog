using Blog.Application.Models;
using Blog.Domain.Entities.Categories;
using MediatR;

namespace Blog.Application.Categories.Queries
{
    public class GetAllCategoriesQuery : IRequest<OperationResult<IEnumerable<Category>>>
    {
    }
}
