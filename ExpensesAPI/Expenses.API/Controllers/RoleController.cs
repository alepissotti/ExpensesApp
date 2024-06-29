using Expenses.Application.Features.Roles.Queries;
using Expenses.Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Expenses.API.Controllers
{
    [Route("api/role")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RoleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<List<RoleDTO>> GetAll()
        {
            List<RoleDTO> response = await _mediator.Send(new GetRolesQuery());

            return response;
        }
    }
}
