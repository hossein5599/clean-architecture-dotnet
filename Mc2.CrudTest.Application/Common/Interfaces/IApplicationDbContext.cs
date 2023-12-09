using Mc2.CrudTest.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Mc2.CrudTest.Application.Common.Interfaces;
public interface IApplicationDbContext
{
    public DbSet<Customer> Customers { get; }
 
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}