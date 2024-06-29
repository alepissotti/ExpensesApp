﻿using Expenses.Application.Features.Accounts.Queries;
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
    }
}