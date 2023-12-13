using Mc2.CrudTest.Api.Controllers;
using Mc2.CrudTest.Application.Common.Exceptions;
using Mc2.CrudTest.Application.Common.Models;
using Mc2.CrudTest.Application.Customers.Commands.Create;
using Mc2.CrudTest.Application.Customers.Commands.Delete;
using Mc2.CrudTest.Application.Customers.Commands.Update;
using Mc2.CrudTest.Application.Customers.Queries;
using Mc2.CrudTest.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Mc2.CrudTest.UnitTests.ControllersTests;
public class CustomerControllerTests
{
    private readonly Mock<ILogger<CustomerController>> _loggerMock;
    private readonly CustomerController _controller;

    private readonly Mock<ISender> _mediatorMock;

    public CustomerControllerTests()
    {
        _loggerMock = new Mock<ILogger<CustomerController>>();
        _controller = new CustomerController(_loggerMock.Object);
        _mediatorMock = new Mock<ISender>();
        var httpContext = new Mock<HttpContext>();
        httpContext.Setup(x => x.RequestServices.GetService(typeof(ISender)))
            .Returns(_mediatorMock.Object);
        _controller.ControllerContext.HttpContext = httpContext.Object;
    }   

    [Fact]
    public async Task CreateCustomer_ReturnsOkObjectResult()
    {
       
        // Arrange
        var customer = new CreateCustomerCommand()
        {
            FirsName = "a",
            Lastname = "b",
            DateOfBirth = DateTime.Now.AddYears(-34),
            BankAccountNumber = "6765431782",
            Email = "m.hossein.sadeghi@gmail.com",
            PhoneNumber = "+13478093374"
        };
        var expectedId = 1; 

        _mediatorMock.Setup(x => x.Send(It.IsAny<CreateCustomerCommand>(), default))
           .ReturnsAsync(expectedId);

        // Act
        var result = await _controller.Create(customer);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var customerId = Assert.IsType<int>(okResult.Value);
        Assert.Equal(1, customerId);

    }

    [Fact]
    public async Task Create_InvalidData_ReturnsBadRequestResult()
    {
        // Arrange
        var command = new CreateCustomerCommand { /* Set wrong command properties */ };

        _mediatorMock.Setup(m => m.Send(command, default)); //ThrowsAsync(new ValidationException("Invalid data"));

        // Act
        var result = await _controller.Create(command);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result.Result);
    }


    [Fact]
    public async Task Update_ValidData_ReturnsOkResult()
    {
        // Arrange
        var command = new UpdateCustomerCommand
        {
            Id =1,
            Firstname = "a",
            Lastname = "b",
            DateOfBirth = DateTime.Now.AddYears(-34),
            BankAccountNumber = "6765431782",
            Email = "m.hossein.sadeghi@gmail.com",
            PhoneNumber = "+13478090374"
        };

        _mediatorMock.Setup(x => x.Send(command, default));
        // Act
        var result = await _controller.Update(1, command);

        // Assert
        var okResult = Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task Update_InvalidData_ReturnsBadRequestResult()
    {
        // Arrange
        var command = new UpdateCustomerCommand { /* Set command properties */ };

        _mediatorMock.Setup(m => m.Send(command, default)); //.ThrowsAsync(new NotFoundException("Item not found"));

        // Act
        var result = await _controller.Update(1,command);

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }


    [Fact]
    public async Task Delete_ExistingId_ReturnsOkResult()
    {
        // Arrange
        var id = 1;

        _mediatorMock.Setup(x => x.Send(It.IsAny<DeleteCustomerCommand>(), default)); //.Verifiable();

        // Act
        var result = await _controller.Delete(id);

        // Assert
        _mediatorMock.Verify();
        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task Delete_NonExistingId_ReturnsInternalServerError()
    {
        // Arrange
        var id = 13;

        _mediatorMock.Setup(x => x.Send(It.IsAny<DeleteCustomerCommand>(), default)).Throws<Exception>();

        // Act
        var result = await _controller.Delete(id);

        // Assert
        Assert.IsType<StatusCodeResult>(result);
        var statusCodeResult = (StatusCodeResult)result;
        Assert.Equal(500, statusCodeResult.StatusCode);
    }



    [Fact]
    public async Task GetById_ExistingId_ReturnsActionResulttWithModel()
    {
        // Arrange
        var id = 1;
        var expectedModel = new CustomerDto
        {
            FirstName = "a",
            LastName = "b",
            Email = "m.hossein.sadeghi@gmail.com",
        };

        _mediatorMock.Setup(m => m.Send(It.IsAny<GetCustomerQuery>(), default)).ReturnsAsync(expectedModel);

        // Act
        var result = await _controller.GetCustomer(id);

        // Assert
        var okObjectResult = Assert.IsType<OkObjectResult>(result.Result);
        var model = Assert.IsAssignableFrom<CustomerDto>(okObjectResult.Value);
        Assert.Equal(expectedModel.FirstName, model.FirstName);
        Assert.Equal(expectedModel.LastName, model.LastName);
        Assert.Equal(expectedModel.Email, model.Email);
    }

    [Fact]
    public async Task GetById_NonExistingId_ReturnsBadRequestResult()
    {
        // Arrange
        var id = 13;


        _mediatorMock.Setup(m => m.Send(It.IsAny<GetCustomerQuery>(), default));

        // Act
        var result = await _controller.GetCustomer(id);

        // Assert
        Assert.IsType<NoContentResult>(result.Result);
    }

   
}