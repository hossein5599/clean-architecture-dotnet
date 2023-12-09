using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mc2.CrudTest.Domain.Entities;
using System.Reflection.Emit;

namespace Mc2.CrudTest.Infrastructure.Persistence.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customer");

        builder.Ignore(e => e.DomainEvents);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.Property(p => p.RowVersion)
           .IsConcurrencyToken()
           .ValueGeneratedOnAddOrUpdate();

        builder
            .HasIndex(c => new { c.Firstname, c.Lastname, c.DateOfBirth })
            .IsUnique();

        builder
                .HasIndex(c => c.Email)
              .IsUnique();
    }
}
