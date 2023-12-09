using Mc2.CrudTest.Domain.Common;

namespace Mc2.CrudTest.Domain.Entities;

public class Customer : AuditableEntity, IHasDomainEvent
{
    public int Id { get; set; }
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }
    public DateTime DateOfBirth { get; set; }
    public ulong? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? BankAccountNumber { get; set; }

    public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
}

