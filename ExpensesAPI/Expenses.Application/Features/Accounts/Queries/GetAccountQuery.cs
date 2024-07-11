using Expenses.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Expenses.Domain.Entities;
using FluentValidation;
using Expenses.Application.Exceptions;
using Expenses.Domain;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Expenses.Domain.Dtos;

namespace Expenses.Application.Features.Accounts.Queries
{

    public class GetAccountQueryValidator : AbstractValidator<GetAccountQuery>
    {
        public GetAccountQueryValidator()
        {
            RuleFor(p => p.Id).NotNull().NotEmpty();
        }
    }
    public class GetAccountQuery: IRequest<AccountDTO>
    {
        public string Id { get; set; }
    }

    public class GetAccountQueryHandler: IRequestHandler<GetAccountQuery, AccountDTO>
    {
        private readonly ExpensesDbContext _context;
        private readonly IMapper _mapper;

        public GetAccountQueryHandler(ExpensesDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AccountDTO> Handle(GetAccountQuery request, CancellationToken cancellationToken)
        {
            Account account = await _context.Accounts
                                            .FirstOrDefaultAsync(a =>
                                                                    a.Id == Hashid.Decode(request.Id) &&
                                                                    (!a.Deleted.HasValue || !a.Deleted.Value)
                                                                );

            if (account == null)
            {
                throw new NotFoundException(nameof (Account), request.Id);
            }

            return _mapper.Map<AccountDTO>(account);
        }
    }

}
