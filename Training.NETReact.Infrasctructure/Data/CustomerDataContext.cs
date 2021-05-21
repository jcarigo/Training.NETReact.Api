using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;
using Training.NETReact.Domain.Models;

namespace Training.NETReact.Infrasctructure.Data
{
    public class CustomerDataContext : DbContext, ICustomerDataContext
    {
        private IDbContextTransaction _transaction;        

        public DbSet<Customer> Customer { get; set; }

        public CustomerDataContext(DbContextOptions<CustomerDataContext> options) : base(options) { }

        public async Task BeginTransactionAsync()
        {
            _transaction = await Database.BeginTransactionAsync();
        }

        public void BeginTransation()
        {
            _transaction = Database.BeginTransaction();
        }

        public void Commit()
        {
            try
            {
                SaveChanges();
                _transaction.Commit();
            }
            catch
            {
                RollBack();
            }
        }

        public async Task CommitAsync()
        {
            await SaveChangesAsync();
            await _transaction.CommitAsync();
        }
         
        public async Task RollbackAsync()
        {
            await _transaction.RollbackAsync();
            _transaction.Dispose();
        }

        public void RollBack()
        {
            _transaction.Rollback();
            _transaction.Dispose();
        }

        public CustomerDataContext GetCustomerDataContext() => this;
    }

    public interface ICustomerDataContext : IUnitOfWork
    {
        DbSet<Domain.Models.Customer> Customer { get; set; }
        CustomerDataContext GetCustomerDataContext();

    }
    public interface IUnitOfWork
    {
        public void Commit();
        public void BeginTransation();
        public void RollBack();

        public Task CommitAsync();
        public Task BeginTransactionAsync();
        public Task RollbackAsync();
    }
}
