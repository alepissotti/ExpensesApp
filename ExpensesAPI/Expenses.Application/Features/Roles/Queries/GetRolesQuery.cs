using AutoMapper;
using AutoMapper.QueryableExtensions;
using Expenses.Domain.Dtos;
using Expenses.Domain.Entities;
using Expenses.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Expenses.Application.Features.Roles.Queries
{
    public class GetRolesQuery: IRequest<List<RoleDTO>>
    {
    }

    public class GetRolesQueryHandler: IRequestHandler<GetRolesQuery, List<RoleDTO>>
    {
        private readonly ExpensesDbContext _context;
        private readonly IMapper mapper;

        public GetRolesQueryHandler(ExpensesDbContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        public async Task<List<RoleDTO>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            List<RoleDTO> roles = await _context.Roles
                                                .AsNoTracking()
                                                .ProjectTo<RoleDTO>(mapper.ConfigurationProvider)
                                                .ToListAsync();
            
            return roles;
        }
    }
}
