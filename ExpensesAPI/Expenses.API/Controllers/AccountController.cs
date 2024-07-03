using Expenses.Application.Features.Accounts.Commands;
using Expenses.Application.Features.Accounts.Queries;
using Expenses.Domain.Dtos;
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
        public async Task<GetAccountQueryResponse> Get([FromRoute] GetAccountQuery request)
        {
            GetAccountQueryResponse response = await _mediator.Send(request);

            return response;
        }

        [HttpPost]
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
    }
}
