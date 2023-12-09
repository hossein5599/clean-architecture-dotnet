using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Infrastructure.Persistence;

public class ApplicationDbContextSeed
{
    public static void SeedSampleData(ApplicationDbContext context)
    {
        if (!context.Customers.Any())
        {
            context.Customers.AddRange(
                new Domain.Entities.Customer { Id = 1, Firstname = "s", Lastname = "s", PhoneNumber = 00165165678, Email = "m.hossein.sadeghi@gmail.com", DateOfBirth = DateTime.Now }

            );


        }
    }
}
