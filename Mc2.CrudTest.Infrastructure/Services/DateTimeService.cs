using Mc2.CrudTest.Application.Common.Interfaces;

namespace Mc2.CrudTest.Infrastructure.Services;
public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}