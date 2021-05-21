using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.NETReact.Domain.Contracts;
using Training.NETReact.Infrasctructure.Data;

namespace Training.NETReact.Infrasctructure.Queries
{
    public class DeleteCustomerQuery : IQuery<int>
    {
        protected readonly ICustomerDataContext _db;
        protected int _id;

        public DeleteCustomerQuery(ICustomerDataContext context, int id)
        {
            _db = context;
            _id = id;
        }

        public async Task<int> ExecuteQuery()
        {
            try
            {
                await _db.BeginTransactionAsync();
                var customer = _db.Customer.Find(_id);
                _db.Customer.Remove(customer);
                await _db.CommitAsync();
                return _id;
            }
            catch
            {
                await _db.RollbackAsync();
                throw;
            }
        }
    }
}
