using Blog.Application.Identity.Dtos;
using Blog.Application.Models;
using MediatR;

namespace Blog.Application.Identity.Queries
{
    public class GetAllRolesQuery : IRequest<OperationResult<IEnumerable<UserRoleDto>>>
    {
    }
}
