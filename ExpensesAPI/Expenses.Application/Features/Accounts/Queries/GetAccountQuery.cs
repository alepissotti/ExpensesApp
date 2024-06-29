using Expenses.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Expenses.Domain.Entities;
using FluentValidation;
using Expenses.Application.Exceptions;

namespace Expenses.Application.Features.Accounts.Queries
{

    public class GetAccountQueryValidator : AbstractValidator<GetAccountQuery>
    {
        public GetAccountQueryValidator()
        {
            RuleFor(p => p.Id).NotNull().GreaterThanOrEqualTo(1);
        }
    }
    public class GetAccountQuery: IRequest<GetAccountQueryResponse>
    {
        public int Id { get; set; }
    }

    public class GetAccountQueryHandler: IRequestHandler<GetAccountQuery, GetAccountQueryResponse>
    {
        private readonly ExpensesDbContext _context;

        public GetAccountQueryHandler(ExpensesDbContext context)
        {
            _context = context;
        }

        public async Task<GetAccountQueryResponse> Handle(GetAccountQuery request, CancellationToken cancellationToken)
        {
            Account account = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == request.Id && !a.Deleted.Value);

            if (account == null)
            {
                throw new NotFoundException(nameof (Account), request.Id);
            }

            return new GetAccountQueryResponse
            {
                FirstName = account.FirstName,
                LastName = account.LastName,
                UserName = account.UserName,
                Id = account.Id,
                RoleId = account.RoleId,
            };
        }
    }

    public class GetAccountQueryResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public int RoleId { get; set; }
    }
}
