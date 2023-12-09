using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Domain.Common;


public class DomainEvent
{
    protected DomainEvent()
    {
        DateOccurred = DateTimeOffset.UtcNow;
    }
    public bool IsPublished { get; set; }
    public DateTimeOffset DateOccurred { get; protected set; } = DateTime.UtcNow;
}
public interface IHasDomainEvent
{
    public List<DomainEvent> DomainEvents { get; set; }
}

