using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.NETReact.Domain.Contracts;
using Training.NETReact.Domain.Models;
using Training.NETReact.Infrasctructure.Data;

namespace Training.NETReact.Infrasctructure.Queries
{
    public class UpdateCustomerQuery : IQuery<Domain.Models.Customer>
    {
        protected readonly ICustomerDataContext _db;
        protected Domain.Models.Customer _customer;

        public UpdateCustomerQuery(ICustomerDataContext context, Domain.Models.Customer customer)
        {
            _db = context;
            _customer = customer;
        }

        public async Task<Customer> ExecuteQuery()
        {
            try
            {
                await _db.BeginTransactionAsync();

                var currentData = await _db.Customer.FirstOrDefaultAsync(x => x.Id == _customer.Id);

                if (currentData == null)
                    return null;

                var _dbContext = _db.GetCustomerDataContext();
                _dbContext.Entry(currentData).CurrentValues.SetValues(_customer);

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
