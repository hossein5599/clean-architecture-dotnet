using Mc2.CrudTest.Domain.Common;
using Mc2.CrudTest.Domain.Entities;

namespace Mc2.CrudTest.Domain.Events;
public class CustomerDeletedEvent : DomainEvent
{
    public CustomerDeletedEvent(Customer item)
    {
        Item = item;
    }

    public Customer Item { get; }
}

