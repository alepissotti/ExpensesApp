using AutoMapper;
using Expenses.Domain.Dtos;
using Expenses.Domain.Entities;
using Expenses.Infrastructure.Persistence;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Expenses.Application.Features.Accounts.Queries
{
    public class GetAccountsQueryValidator : AbstractValidator<GetAccountsQuery>
    {
        public GetAccountsQueryValidator()
        {
            RuleFor(p => p.Page).NotEmpty().NotNull().GreaterThanOrEqualTo(1);
            RuleFor(p => p.PageSize).NotEmpty().NotNull().GreaterThanOrEqualTo(1);
        }
    }

    public class GetAccountsQuery: IRequest<List<AccountDTO>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }

    public class GetAccountsQueryHandler : IRequestHandler<GetAccountsQuery, List<AccountDTO>> { 
        
        private ExpensesDbContext _context;
        private IMapper _mapper;

        public GetAccountsQueryHandler(ExpensesDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<AccountDTO>> Handle(GetAccountsQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Account> accounts = _context.Accounts.Where(x => !x.Deleted.HasValue || !x.Deleted.Value).AsQueryable();

            List<Account> result = await accounts.Skip((request.Page - 1) * request.PageSize)
                                                 .Take(request.PageSize)
                                                 .ToListAsync();

            return _mapper.Map<List<AccountDTO>>(result);


        }
    }
}
