using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.NETReact.Infrasctructure.Data;
using Training.NETReact.Domain.Contracts;
using Training.NETReact.Domain.Models;

namespace Training.NETReact.Infrasctructure.Queries
{
    public class AddCustomerQuery : IQuery<Domain.Models.Customer>
    {
        protected readonly ICustomerDataContext _db;
        protected Domain.Models.Customer _customer;
        
        public AddCustomerQuery(ICustomerDataContext context, Domain.Models.Customer customer)
        {
            _db = context;
            _customer = customer;
        }


        public async Task<Customer> ExecuteQuery()
        {
            try
            {
                await _db.BeginTransactionAsync();
                _db.Customer.Add(_customer);
                await _db.CommitAsync();

                return _customer;
            }
            catch
            {
                await _db.RollbackAsync();
                throw;
            }
        }
    }
}
