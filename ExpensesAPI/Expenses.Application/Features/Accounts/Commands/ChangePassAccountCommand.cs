using Expenses.Application.Exceptions;
using Expenses.Domain;
using Expenses.Domain.Dtos;
using Expenses.Domain.Entities;
using Expenses.Infrastructure.Persistence;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace Expenses.Application.Features.Accounts.Commands
{
    public class ChangePassAccountValidator : AbstractValidator<ChangePassAccountCommand>
    {
        public ChangePassAccountValidator()
        {
            RuleFor(p => p.Id).NotEmpty().NotNull();
            RuleFor(p => p.OldPassword).NotEmpty().NotNull();
            RuleFor(p => p.NewPassword).NotEmpty().NotNull();
            RuleFor(p => p.RepeatNewPassword).NotEmpty().NotNull().Equal(p => p.NewPassword);
        }
    }
    public class ChangePassAccountCommand: IRequest<ChangePassAccountCommandResponse>
    {
        public string Id { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string RepeatNewPassword { get; set; }
    }

    public class ChangePassAccountCommandResponse
    {
        public string Id { get; set; }
        public string Message { get; set; }
    }

    public class ChangePassAccountCommandHandler: IRequestHandler<ChangePassAccountCommand, ChangePassAccountCommandResponse>
    {
        private readonly ExpensesDbContext _context;

        public ChangePassAccountCommandHandler(ExpensesDbContext expensesDbContext)
        {
            _context = expensesDbContext;
        }

        public async Task<ChangePassAccountCommandResponse> Handle(ChangePassAccountCommand request, CancellationToken cancellationToken)
        {
            Account accountUpdated = await _context.Accounts
                                                   .FirstOrDefaultAsync(acc =>
                                                                              acc.Id.Equals(Hashid.Decode(request.Id)) &&
                                                                              (!acc.Deleted.HasValue || !acc.Deleted.Value)
                                                                       );

            if (accountUpdated is null)
                throw new NotFoundException(nameof(Account), request.Id);

            bool isCorrectPassword = BCrypt.Net.BCrypt.Verify(request.OldPassword, accountUpdated.Password);
            if (!isCorrectPassword)
            {
                List<FluentValidation.Results.ValidationFailure> failures = new List<FluentValidation.Results.ValidationFailure>();
                failures.Add(new FluentValidation.Results.ValidationFailure { PropertyName = "Password", ErrorMessage = "La contraseña actual no es correcta" });
                throw new Exceptions.ValidationException(failures);
            }

            accountUpdated.Password = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
            _context.Accounts.Update(accountUpdated);
            await _context.SaveChangesAsync();

            return new ChangePassAccountCommandResponse
            {
                Id = request.Id,
                Message = "La contraseña ha sido cambiada exitosamente"
            };
        }
    }
}
