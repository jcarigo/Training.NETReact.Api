using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.NETReact.Domain.Contracts
{
    public interface ICustomeRepository
    {
        Task<Domain.Models.Customer> AddCustomer(Domain.Models.Customer customer);
        Task<Models.Customer> UpdateCustomer(Models.Customer customer);
        Task<int> DeleteCustomer(int id);
        Task<Models.Customer> GetCustomerById(int id);
        Task<IEnumerable<Models.Customer>> GetAllCustomers();
    }
}
