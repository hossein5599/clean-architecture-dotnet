using Mc2.CrudTest.Application.Common.Models;
using Mc2.CrudTest.Application.Customers.Commands.Create;
using Mc2.CrudTest.Application.Customers.Commands.Delete;
using Mc2.CrudTest.Application.Customers.Commands.Update;
using Mc2.CrudTest.Application.Customers.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Mc2.CrudTest.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ApiControllerBase
    {

        private readonly ILogger<CustomerController> _logger;
     
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public CustomerController(ILogger<CustomerController> logger/*, IMediator mediator*/) // : base(mediator)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            _logger = logger;
        }

        [HttpGet]


        [HttpGet]
        public async Task<ActionResult<PaginatedList<CustomerDto>>> GetCustomersWithPagination([FromQuery] GetCustomerWithPaginationQuery query)
        {
            return await Mediator.Send(query);
            //return await _mediator.Send(query);
            
         
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CustomerDto>> GetCustomer(int id)
        {
            var res = await Mediator.Send(new GetCustomerQuery { Id = id });

            try
            {
                if (res != null)
                    return Ok(res);
                else
                    return NoContent();
            }
            catch
            {
                return BadRequest();

            }
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromForm] CreateCustomerCommand command)
        {

            // your code to create the customer
            var customerId = await Mediator.Send(command);

            if (customerId != 0)
            {
                // return OkObjectResult with the customer ID
                return Ok(customerId);
            }
            else
            {
                // return BadRequestResult or any other appropriate result
                return BadRequest("Failed to create customer");
            }

           // return await Mediator.Send(command);
            //return await _mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateCustomerCommand command)
        {
            if (id == command.Id)
            {
                await Mediator.Send(command);
                return Ok();
            }
            else
            {
                return BadRequest();

            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        { 
            try
            {
                await Mediator.Send(new DeleteCustomerCommand { Id = id });
                return Ok(); // Return Ok for success
            }
            catch (Exception)
            {
                return StatusCode(500); // Return 500 Internal Server Error for failure
            }
        }
    }
}
