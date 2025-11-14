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
    public class PurchaseDetailRepository : IPurchaseDetailRepository
    {
        private readonly AppDBContext context;

        public PurchaseDetailRepository(AppDBContext context)
        {
            this.context = context;
        }

        public List<PurchaseDetails> GetPurchasesWithinDateRange(DateTime from, DateTime to)
        {
            return context.PurchaseDetails
                .Include(pd => pd.Purchase)
                .Where(pd => pd.Purchase.PurchaseDate >= from && pd.Purchase.PurchaseDate <= to)
                .ToList();
        }
    }
}
