using AutoMapper;
using Mc2.CrudTest.Application.Common.Interfaces;
using Mc2.CrudTest.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Application.Customers.Queries;

public class GetCustomerQuery : IRequest<CustomerDto>
{
    public int Id { get; set; }
}

public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, CustomerDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCustomerQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CustomerDto> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Customers.FindAsync(request.Id);

        return _mapper.Map<Customer, CustomerDto>(entity!);
    }
}