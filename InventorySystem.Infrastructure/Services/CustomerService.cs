using AutoMapper;
using InventorySystem.Core.DTOs;
using InventorySystem.Core.Entities;
using InventorySystem.Core.Interfaces.Repositories;
using InventorySystem.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Infrastructure.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository cstmRepo;
        private readonly IMapper mapper;

        public CustomerService(ICustomerRepository cstmRepo, IMapper mapper)
        {
            this.cstmRepo = cstmRepo;
            this.mapper = mapper;
        }

        public void AddCustomer(CustomerDto dto)
        {
            cstmRepo.Add(mapper.Map<Customer>(dto));
            cstmRepo.SaveChanges();
        }

        public void DeleteCustomer(int id)
        {
            cstmRepo.Delete(id);
            cstmRepo.SaveChanges();
        }

        public List<CustomerDto> GetAllCustomers(int size = 20, int pageNumber = 1)
        {
            var cstm = cstmRepo.GetAll(size, pageNumber);
            return mapper.Map<List<CustomerDto>>(cstm);
        }

        public CustomerDto? GetCustomerById(int id)
        {
            var cstm = cstmRepo.GetByID(id);

            if (cstm != null)
            {
                return mapper.Map<CustomerDto>(cstm);
            }
            else
            {
                return null;
            }
        }

        public void UpdateCustomer(CustomerDto dto)
        {
            cstmRepo.Update(mapper.Map<Customer>(dto));
            cstmRepo.SaveChanges();
        }
    }
}
