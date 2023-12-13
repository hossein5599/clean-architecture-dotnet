using Mc2.CrudTest.Application.Common.Models;
using Mc2.CrudTest.Application.Customers.Commands.Create;
using Mc2.CrudTest.Application.Customers.Commands.Delete;
using Mc2.CrudTest.Application.Customers.Commands.Update;
using Mc2.CrudTest.Application.Customers.Queries;
using Mc2.CrudTest.Presentation.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Mc2.CrudTest.Presentation.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public CustomerController(ILogger<WeatherForecastController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

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