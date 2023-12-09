using Mc2.CrudTest.Domain.Common;


namespace Mc2.CrudTest.Application.Common.Interfaces;
public interface IDomainEventService
{
    Task Publish(DomainEvent domainEvent);
}
