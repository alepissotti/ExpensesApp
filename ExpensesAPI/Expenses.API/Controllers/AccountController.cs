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

        [HttpGet]
        [AuthorizePermission(PermissionNames.ACCOUNTS_GETALL)]
        public async Task<List<AccountDTO>> GetAll([FromQuery] GetAccountsQuery request)
        {
            List<AccountDTO> response = await _mediator.Send(request);

            return response;
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
        public async Task<LoginAccountQueryResponse> Login([FromBody] LoginAccountQuery request)
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

        [HttpDelete("{Id}")]
        [AuthorizePermission(PermissionNames.ACCOUNTS_DELETE)]
        public async Task<DeleteAccountCommandResponse> Delete([FromRoute] DeleteAccountCommand request)
        {
            DeleteAccountCommandResponse response = await _mediator.Send(request); 
            return response;
        }

        [HttpPut("change_password")]
        [AuthorizePermission(PermissionNames.ACCOUNTS_CHANGE_PASS)]
        public async Task<ChangePassAccountCommandResponse> ChangePassword([FromBody] ChangePassAccountCommand request)
        {
            ChangePassAccountCommandResponse response = await _mediator.Send(request);
            return response;
        }
    }
}
