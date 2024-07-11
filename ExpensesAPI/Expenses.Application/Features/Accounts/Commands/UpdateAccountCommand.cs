using AutoMapper;
using Expenses.Application.Exceptions;
using Expenses.Domain;
using Expenses.Domain.Dtos;
using Expenses.Domain.Entities;
using Expenses.Infrastructure.Persistence;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Expenses.Application.Features.Accounts.Commands
{
    public class UpdateCommandValidator : AbstractValidator<UpdateAccountCommand>
    {
        public UpdateCommandValidator()
        {
            RuleFor(p => p.Id).NotEmpty().NotNull();
            RuleFor(p => p.FirstName).NotEmpty().NotNull().MaximumLength(25);
            RuleFor(p => p.LastName).NotEmpty().NotNull().MaximumLength(25);
            RuleFor(p => p.UserName).NotEmpty().NotNull().MaximumLength(15);
        }
    }
    public class UpdateAccountCommand: IRequest<AccountDTO>
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class UpdateAccountCommandHandler: IRequestHandler<UpdateAccountCommand, AccountDTO>
    {
        private readonly ExpensesDbContext _context;
        private readonly IMapper _mapper;

        public UpdateAccountCommandHandler(ExpensesDbContext expensesDbContext, IMapper mapper)
        {
            _context = expensesDbContext;
            _mapper = mapper;
        }

        public async Task<AccountDTO> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
        {
            Account userUpdated = await _context.Accounts
                                                .FirstOrDefaultAsync(acc => 
                                                                           acc.Id.Equals(Hashid.Decode(request.Id)) &&
                                                                           (!acc.Deleted.HasValue || !acc.Deleted.Value)
                                                                    );
            if (userUpdated is null)
                throw new NotFoundException(nameof(Account), request.Id);
            
            bool existUserName = await _context.Accounts
                                               .Where(acc =>
                                                            acc.Id != Hashid.Decode(request.Id) &&
                                                            acc.UserName.Equals(request.UserName) &&
                                                            (!acc.Deleted.HasValue || !acc.Deleted.Value)
                                                     )
                                               .AnyAsync();

            if (existUserName) {
                List<FluentValidation.Results.ValidationFailure> failures = new List<FluentValidation.Results.ValidationFailure>();
                failures.Add(new FluentValidation.Results.ValidationFailure { PropertyName = "Username", ErrorMessage = "El usuario ya se encuentra registrado" });
                throw new Exceptions.ValidationException(failures);
            }

            userUpdated.UserName = request.UserName;
            userUpdated.FirstName = request.FirstName;
            userUpdated.LastName = request.LastName;

            _context.Accounts.Update(userUpdated);
            await _context.SaveChangesAsync();

            return _mapper.Map<AccountDTO>(userUpdated);   
            
        }
    }
}
