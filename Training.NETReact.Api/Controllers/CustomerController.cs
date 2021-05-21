using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training.NETReact.Application.CQRS.Commands.AddCustomer;
using Training.NETReact.Application.CQRS.Commands.DeleteCustomer;
using Training.NETReact.Application.CQRS.Commands.UpdateCustomer;
using Training.NETReact.Application.CQRS.Queries.GetAllCustomer;    
using Training.NETReact.Application.CQRS.Queries.GetCustomerById;

namespace Training.NETReact.Api.Controllers
{
    [Route("api/Customer")]
    [Authorize]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator) => _mediator = mediator;

        [HttpPost("insert")]
        public async Task<IActionResult> AddCustomer([FromBody] AddCustomerCommand customer)
        {
            if(customer.LastName.Trim().Length > 0 && customer.FirstName.Trim().Length > 0)
                 return Ok(await _mediator.Send(customer));

            return BadRequest("Firstname and Lastname are required.");
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllCustomer() => Ok(await _mediator.Send(new GetAllCustomerQuery()));

        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetCustomerById([FromQuery] int id) 
        {
            var result = await _mediator.Send(new GetCustomerByIdQuery { Id = id });
            return Ok(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateCustomer([FromBody] UpdateCustomerCommand customer)
        {
            if(customer.Customer.FirstName.Trim().Length > 0 && customer.Customer.LastName.Trim().Length > 0)
                return Ok(await _mediator.Send(customer));

            return BadRequest("Firstname and Lastname are required.");
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteCustomer([FromQuery] int id) => Ok(await _mediator.Send(new DeleteCustomerCommand { Id = id }));
    }
}
