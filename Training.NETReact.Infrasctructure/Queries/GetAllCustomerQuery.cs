using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Training.NETReact.Domain.Contracts;
using Training.NETReact.Domain.Models;
using Training.NETReact.Infrasctructure.Data;

namespace Training.NETReact.Infrasctructure.Queries
{
    public class GetAllCustomerQuery : IQuery<IEnumerable<Domain.Models.Customer>>
    {
        protected readonly ICustomerDataContext _db;

        public GetAllCustomerQuery(ICustomerDataContext context) => _db = context;

        public async Task<IEnumerable<Customer>> ExecuteQuery()
        {
            return await _db.Customer.ToListAsync();
        }
    }
}
