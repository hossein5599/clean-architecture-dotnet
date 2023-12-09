using Mc2.CrudTest.Application.Common.Exceptions;
using Mc2.CrudTest.Application.Common.Interfaces;
using Mc2.CrudTest.Domain.Entities;
using MediatR;

namespace Mc2.CrudTest.Application.Customers.Commands.Update;
public class UpdateCustomerCommand : IRequest
{
    public int Id { get; set; }
    public string? Firstname { get; set; } = String.Empty;
    public string? Lastname { get; set; } = String.Empty;
    public DateTime DateOfBirth { get; set; }
    public string? PhoneNumber { get; set; } = String.Empty;
    public string? Email { get; set; } = String.Empty;
    public string? BankAccountNumber { get; set; } = String.Empty;


}

public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateCustomerCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Customers
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Customer), request.Id);
        }

        entity.Firstname = string.IsNullOrEmpty(request.Firstname) ? "" :  request.Firstname;
        entity.Lastname = request.Lastname;
        entity.Email  = request.Email;
       

        await _context.SaveChangesAsync(cancellationToken);

       // return Unit.Value;
    }


}
