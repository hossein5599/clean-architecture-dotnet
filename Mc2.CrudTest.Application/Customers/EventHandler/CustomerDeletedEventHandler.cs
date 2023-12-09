using Mc2.CrudTest.Application.Common.Models;
using Mc2.CrudTest.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Mc2.CrudTest.Application.Customers.EventHandler;
 
public class CustomerDeletedEventHandler : INotificationHandler<DomainEventNotification<CustomerDeletedEvent>>
{
    private readonly ILogger<CustomerDeletedEventHandler> _logger;

    public CustomerDeletedEventHandler(ILogger<CustomerDeletedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(DomainEventNotification<CustomerDeletedEvent> notification, CancellationToken cancellationToken)
    {
        var domainEvent = notification.DomainEvent;

        _logger.LogInformation("Domain Event: {DomainEvent}", domainEvent.GetType().Name);

        return Task.CompletedTask;
    }
}
