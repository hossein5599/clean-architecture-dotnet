using Mc2.CrudTest.Application.Common.Models;
using Mc2.CrudTest.Application.Customers.Commands.Create;
using Mc2.CrudTest.Application.Customers.Commands.Delete;
using Mc2.CrudTest.Application.Customers.Commands.Update;
using Mc2.CrudTest.Application.Customers.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mc2.CrudTest.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ApiControllerBase
    {

        private readonly ILogger<WeatherForecastController> _logger;

        public CustomerController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }
        [HttpGet]


        [HttpGet]
        public async Task<ActionResult<PaginatedList<CustomerDto>>> GetSuperheroesWithPagination([FromQuery] GetCustomerWithPaginationQuery query)
        {
            return await Mediator.Send(query);
         
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CustomerDto>> GetSuperhero(int id)
        {
            return await Mediator.Send(new GetCustomerQuery { Id = id });
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromForm] CreateCustomerCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateCustomerCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteCustomerCommand { Id = id });

            return NoContent();
        }
    }
}
