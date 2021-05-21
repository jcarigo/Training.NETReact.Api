using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.NETReact.Domain.Contracts;
using Training.NETReact.Domain.Models;
using Training.NETReact.Infrasctructure.Data;

namespace Training.NETReact.Infrasctructure.Queries
{
    public class GetCustomerByIdQuery : IQuery<Domain.Models.Customer>
    {
        protected readonly ICustomerDataContext _db;
        protected readonly int _id;

        public GetCustomerByIdQuery(ICustomerDataContext context, int id)
        {
            _db = context;
            _id = id;
        }

        public async Task<Customer> ExecuteQuery()
        {
            return await _db.Customer.FindAsync(_id);
        }
    }
}
