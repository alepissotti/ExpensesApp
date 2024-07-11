using Expenses.Application.Attributes;
using Expenses.Application.Features.Accounts.Commands;
using Expenses.Application.Features.Accounts.Queries;
using Expenses.Domain.Dtos;
using Expenses.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Expenses.API.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{Id}")]
        [AuthorizePermission(PermissionNames.ACCOUNTS_GET)]
        public async Task<AccountDTO> Get([FromRoute] GetAccountQuery request)
        {
            AccountDTO response = await _mediator.Send(request);

            return response;
        }

        [HttpPost]
        [AuthorizePermission(PermissionNames.ACCOUNTS_ADD)]
        public async Task<AccountDTO> Post([FromBody] CreateAccountCommand request)
        {
            AccountDTO response = await _mediator.Send(request);
            return response;
        }

        [HttpPost("login")]
        public async Task<LoginAccountQueryResponse> Login([FromQuery] LoginAccountQuery request)
        {
            LoginAccountQueryResponse response = await _mediator.Send(request);

            return response;
        }

        [HttpPut]
        [AuthorizePermission(PermissionNames.ACCOUNTS_UPDATE)]
        public async Task<AccountDTO> Put([FromBody] UpdateAccountCommand request)
        {
            AccountDTO response = await _mediator.Send(request);
            return response;
        }
    }
}
