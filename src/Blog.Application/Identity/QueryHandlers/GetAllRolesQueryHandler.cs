using Blog.Application.Identity.Dtos;
using Blog.Application.Identity.Queries;
using Blog.Application.Models;
using Blog.Dal.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blog.Application.Identity.QueryHandlers
{
    public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, OperationResult<IEnumerable<UserRoleDto>>>
    {
        private readonly DataContext _context;
        private readonly OperationResult<IEnumerable<UserRoleDto>> _result;
        public GetAllRolesQueryHandler(DataContext context)
        {
            _context = context;
            _result = new OperationResult<IEnumerable<UserRoleDto>>();
        }

        public async Task<OperationResult<IEnumerable<UserRoleDto>>> Handle(GetAllRolesQuery request, 
            CancellationToken cancellationToken)
        {
            try
            {
                _result.Payload = await _context.Roles.AsNoTracking()
                                                .Select(r=> new UserRoleDto
                                                {
                                                    Name = r.Name
                                                }).ToListAsync();
            }
            catch (Exception ex)
            {
                _result.AddUnknownError($"{ex.Message}");
            }

            return _result;
        }
    }
}
