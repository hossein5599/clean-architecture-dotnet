using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Mc2.CrudTest.Application.Common.Interfaces;
using Mc2.CrudTest.Domain.Entities;
using Mc2.CrudTest.Domain.Events;
using MediatR;

namespace Mc2.CrudTest.Application.Customers.Commands.Create;

public class CreateCustomerCommand : IRequest<int>
{
    public string FirsName { get; set; } = String.Empty;
    public string? Lastname { get; set; } = String.Empty;
    public DateTime DateOfBirth { get; set; }

    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? BankAccountNumber { get; set; }

}

public class CustomerCommandHandler : IRequestHandler<CreateCustomerCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CustomerCommandHandler(IApplicationDbContext context) => _context = context;

    public async Task<int> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        Customer entity = new()
        {
            Firstname = string.IsNullOrEmpty(request.FirsName) ? "" : request.FirsName,
            Lastname = string.IsNullOrEmpty(request.Lastname) ? "" : request.Lastname,
            Email = string.IsNullOrEmpty(request.Email) ? "" : request.Email,
            BankAccountNumber = string.IsNullOrEmpty(request.BankAccountNumber) ? "" : request.BankAccountNumber,
            DateOfBirth = request.DateOfBirth,
            PhoneNumber = ulong.Parse(request.PhoneNumber!)

        };

        entity.DomainEvents.Add(new CustomerCreatedEvent(entity));

        _context.Customers.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}