using InventorySystem.Core.Entities;
using InventorySystem.Core.Interfaces.Repositories;
using InventorySystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InventorySystem.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDBContext context;

        public CustomerRepository(AppDBContext context)
        {
            this.context = context;
        }
        public void Add(Customer obj)
        {
            context.Add(obj);
        }

        public void Delete(int id)
        {
            var cstm = context.Customers.Find(id);
            if (cstm != null)
            {
                context.Customers.Remove(cstm);
            }
        }

        List<Customer> IRepository<Customer>.GetAll(int size, int pageNumber, string includes)
        {
            IQueryable<Customer> customers = context.Customers;

            if (!String.IsNullOrEmpty(includes))
            {
                customers = customers.Include(includes);
            }
            return customers.Skip((pageNumber - 1) * size)
                .Take(size)
                .AsNoTracking()
                .ToList();
        }

        public Customer GetByID(int id, string includes)
        {
            IQueryable<Customer> customer = context.Customers;

            if (!String.IsNullOrEmpty(includes))
            {
                customer = customer.Include(includes);
            }
            return customer.FirstOrDefault(p => p.CustomerId == id);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        void IRepository<Customer>.Update(Customer obj)
        {
            var cstm = context.Customers.FirstOrDefault(c => c.CustomerId == obj.CustomerId);

            if (cstm != null)
            {
                cstm.Name = obj.Name;
                cstm.Email = obj.Email;
                cstm.Address = obj.Address;
                cstm.Phone = obj.Phone;
            }
        }

        public int GetTotalCount()
        {
            return context.Customers.Count();
        }
    }
}
