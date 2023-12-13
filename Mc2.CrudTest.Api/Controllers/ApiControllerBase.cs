using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Mc2.CrudTest.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public abstract class ApiControllerBase : ControllerBase
{
    private ISender _mediator = null!;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();

    //private readonly ISender _mediator;

    //protected ApiControllerBase()
    //{
    //    _mediator = HttpContext.RequestServices.GetRequiredService<ISender>();
    //}

    //protected ISender Mediator => _mediator;
}
