using InventorySystem.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Core.Interfaces.Services
{
    public interface ICustomerService
    {
        PaginationDto<CustomerDto> GetAllCustomers(int size = 20, int pageNumber = 1);
        CustomerDto? GetCustomerById(int id);
        void AddCustomer(CustomerDto dto);
        void UpdateCustomer(CustomerDto dto);
        void DeleteCustomer(int id);
    }
}
