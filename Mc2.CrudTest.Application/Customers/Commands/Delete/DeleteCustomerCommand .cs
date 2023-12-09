using Mc2.CrudTest.Application.Common.Exceptions;
using Mc2.CrudTest.Application.Common.Interfaces;
using Mc2.CrudTest.Domain.Entities;
using Mc2.CrudTest.Domain.Events;
using MediatR;

namespace Mc2.CrudTest.Application.Customers.Commands.Delete;


public class DeleteCustomerCommand : IRequest
{
    public int Id { get; set; }
}

public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteCustomerCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Customers
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Customer), request.Id);
        }

        _context.Customers.Remove(entity);

        entity.DomainEvents.Add(new CustomerDeletedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);

        //return Unit.Value;
    }

   
}