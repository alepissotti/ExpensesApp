using Expenses.Application.Exceptions;
using Expenses.Domain;
using Expenses.Domain.Entities;
using Expenses.Infrastructure.Persistence;
using FluentValidation;
using HashidsNet;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Expenses.Application.Features.Accounts.Commands
{
    public class DeleteAccountCommandResponse
    {
        public string Id { get; set; }
        public string Message { get; set; }
    }

    public class DeleteAccountCommandValidation : AbstractValidator<DeleteAccountCommand>
    {
        public DeleteAccountCommandValidation()
        {
            RuleFor(p => p.Id).NotEmpty().NotNull();
        }
    }
    public class DeleteAccountCommand: IRequest<DeleteAccountCommandResponse>
    {
        public string Id { get; set; }
    }

    public class DeleteAccountCommandHandler: IRequestHandler<DeleteAccountCommand, DeleteAccountCommandResponse>
    {
        private readonly ExpensesDbContext _context;

        public DeleteAccountCommandHandler(ExpensesDbContext context)
        {
            _context = context;
        }

        public async Task<DeleteAccountCommandResponse> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
        {
            Account accountDeleted = await _context.Accounts
                                                   .FirstOrDefaultAsync(x => 
                                                                            x.Id.Equals(Hashid.Decode(request.Id)) && 
                                                                            (!x.Deleted.HasValue || !x.Deleted.Value)
                                                                       );

            if (accountDeleted is null)
                throw new NotFoundException(nameof (Account), request.Id);

            accountDeleted.Deleted = true;
            _context.Accounts.Update(accountDeleted);
            await _context.SaveChangesAsync();

            return new DeleteAccountCommandResponse
            {
                Id = request.Id,
                Message = $"{nameof(Account)} {request.Id} eliminado exitosamente"
            };
        }
    }
}
