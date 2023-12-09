using AutoMapper;
using AutoMapper.QueryableExtensions;
using Mc2.CrudTest.Application.Common.Interfaces;
using Mc2.CrudTest.Application.Common.Mappings;
using Mc2.CrudTest.Application.Common.Models;
using Mc2.CrudTest.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Application.Customers.Queries;

public class CustomerDto : IMapFrom<Customer>
{
    public int Id { get; set; }
    public string FirstName { get; set; } = String.Empty;
    public string LastName { get; set; } = String.Empty;
    public string? Email { get; set; }
    public DateTime Created { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastModified { get; set; }
    public string? LastModifiedBy { get; set; }
    public Guid RowVersion { get; set; }
}

public class GetCustomerWithPaginationQuery : IRequest<PaginatedList<CustomerDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class GetCustomerWithPaginationQueryHandler : IRequestHandler<GetCustomerWithPaginationQuery, PaginatedList<CustomerDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCustomerWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<CustomerDto>> Handle(GetCustomerWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _context.Customers.AsNoTracking()
            .OrderBy(x => x.Firstname)
            .ProjectTo<CustomerDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}