using InventorySystem.Core.Entities;
using InventorySystem.Core.Interfaces.Repositories;
using InventorySystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Infrastructure.Repositories
{
    public class SaleDetailRepository : ISaleDetailRepository
    {
        private readonly AppDBContext context;

        public SaleDetailRepository(AppDBContext context)
        {
            this.context = context;
        }

        public List<SaleDetails> GetSalesWithinDateRange(DateTime from, DateTime to)
        {
            return context.SaleDetails
                .Include(sd => sd.Sales)
                .Where(sd => sd.Sales.SaleDate >= from && sd.Sales.SaleDate <= to)
                .ToList();
        }
    }
}
