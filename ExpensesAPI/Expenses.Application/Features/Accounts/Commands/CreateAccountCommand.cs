﻿using AutoMapper;
using BCrypt.Net;
using Expenses.Domain.Dtos;
using Expenses.Domain.Entities;
using Expenses.Infrastructure.Persistence;
using FluentValidation;
using MediatR;

namespace Expenses.Application.Features.Accounts.Commands
{
    public class CreateCommandValidator : AbstractValidator<CreateAccountCommand>
    {
        public CreateCommandValidator()
        {
            RuleFor(p => p.FirstName).NotEmpty().NotNull().MaximumLength(25);
            RuleFor(p => p.LastName).NotEmpty().NotNull().MaximumLength(25);
            RuleFor(p => p.UserName).NotEmpty().NotNull().MaximumLength(15);
            RuleFor(p => p.RoleId).NotNull().GreaterThanOrEqualTo(1);
        }
    }
    public class CreateAccountCommand: IRequest<AccountDTO>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
    }

    public class CreateAccountCommandHandler: IRequestHandler<CreateAccountCommand,AccountDTO>
    {
        private readonly ExpensesDbContext _context;
        private readonly IMapper _mapper;

        public CreateAccountCommandHandler(ExpensesDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AccountDTO> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            Account newAccount = new Account
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                RoleId = request.RoleId,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = 1
            };

            _context.Add(newAccount);
            await _context.SaveChangesAsync();

            return _mapper.Map<AccountDTO>(newAccount);
        }
    } 
}
