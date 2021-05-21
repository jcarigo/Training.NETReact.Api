using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.NETReact.Infrasctructure.Data;
using Training.NETReact.Infrasctructure.Queries;
using Training.NETReact.Domain.Contracts;
using Training.NETReact.Domain.Models;

namespace Training.NETReact.Infrasctructure.Repository
{
   public  class CustomerRepository : ICustomeRepository
    {
        private readonly ICustomerDataContext _db;
        public CustomerRepository(ICustomerDataContext context) => _db = context;

        public Task<Customer> AddCustomer(Customer customer) => new AddCustomerQuery(_db, customer).ExecuteQuery();

        public Task<int> DeleteCustomer(int id) => new DeleteCustomerQuery(_db, id).ExecuteQuery();

        public Task<IEnumerable<Customer>> GetAllCustomers() => new GetAllCustomerQuery(_db).ExecuteQuery();

        public Task<Customer> GetCustomerById(int id) => new GetCustomerByIdQuery(_db, id).ExecuteQuery();

        public Task<Customer> UpdateCustomer(Customer customer) => new UpdateCustomerQuery(_db, customer).ExecuteQuery();
    }
}
