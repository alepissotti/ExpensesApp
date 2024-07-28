using AutoMapper;
using Expenses.Application.Exceptions;
using Expenses.Domain.Dtos;
using Expenses.Domain.Entities;
using Expenses.Infrastructure.Persistence;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace Expenses.Application.Features.Accounts.Queries
{
    public class LoginAccountQueryValidator : AbstractValidator<LoginAccountQuery>
    {
        public LoginAccountQueryValidator()
        {
            RuleFor(p => p.Username).NotEmpty().NotNull();
            RuleFor(p => p.Password).NotEmpty().NotNull();
        }
    }
    public class LoginAccountQuery:IRequest<LoginAccountQueryResponse>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public class LoginAccountQueryResponse
    {
        public string Token { get; set; }
        public AccountDTO Account { get; set; }
    }

    public class LoginAccountQueryHandler : IRequestHandler<LoginAccountQuery,LoginAccountQueryResponse> { 
        
        private readonly ExpensesDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public LoginAccountQueryHandler(ExpensesDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        private string generateToken(AccountDTO account) {

            var jwtSettings = _configuration.GetSection("jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, JsonSerializer.Serialize(account)),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(double.Parse(jwtSettings["ExpiresInMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<LoginAccountQueryResponse> Handle(LoginAccountQuery request, CancellationToken cancellationToken)
        {
            Account account = await _context.Accounts
                                            .Include(acc => acc.Role)
                                            .Include(acc => acc.AccountPermissions)
                                            .ThenInclude(ap => ap.Permission)
                                            .SingleOrDefaultAsync(acc => acc.UserName.Equals(request.Username));

            if (account is null)
                throw new NotFoundException("No se ha localizado el usuario");

            bool isCorrectPassword = BCrypt.Net.BCrypt.Verify(request.Password, account.Password);
            if (!isCorrectPassword)
            {
                List<FluentValidation.Results.ValidationFailure> failures = new List<FluentValidation.Results.ValidationFailure>();
                failures.Add(new FluentValidation.Results.ValidationFailure { PropertyName = "Password", ErrorMessage = "Las contraseñas no coinciden" });
                throw new Exceptions.ValidationException(failures);               
            }

            //Generación del token
            AccountDTO accountDTO = _mapper.Map<AccountDTO>(account);
            LoginAccountQueryResponse response = new LoginAccountQueryResponse
            {
                Token = generateToken(accountDTO),
                Account = accountDTO
            };

            return response;
        }
    }
}
