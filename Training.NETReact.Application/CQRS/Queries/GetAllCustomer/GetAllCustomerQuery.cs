using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.NETReact.Application.Dtos;

namespace Training.NETReact.Application.CQRS.Queries.GetAllCustomer
{
    public class GetAllCustomerQuery : IRequest<IEnumerable<CustomerDto>>
    {
    }
}
